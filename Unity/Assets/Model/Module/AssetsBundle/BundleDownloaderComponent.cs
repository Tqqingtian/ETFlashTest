﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ETModel
{
	[ObjectSystem]
	public class UiBundleDownloaderComponentAwakeSystem : AwakeSystem<BundleDownloaderComponent>
	{
		public override void Awake(BundleDownloaderComponent self)
		{
			self.bundles = new Queue<string>();
			self.downloadedBundles = new HashSet<string>();
			self.downloadingBundle = "";
		}
	}

	/// <summary>
	/// 用来对比web端的资源，比较md5，对比下载资源
	/// </summary>
	public class BundleDownloaderComponent : Component
	{
		private VersionConfig remoteVersionConfig;
		
		public Queue<string> bundles;

		public long TotalSize;

		public HashSet<string> downloadedBundles;

		public string downloadingBundle;

		public UnityWebRequestAsync webRequest;
		
		public override void Dispose()
		{
				if (this.IsDisposed)
				{
						return;
				}

				if (this.Entity.IsDisposed)
				{
						return;
				}

				base.Dispose();

				this.remoteVersionConfig = null;
				this.TotalSize = 0;
				this.bundles = null;
				this.downloadedBundles = null;
				this.downloadingBundle = null;
				this.webRequest?.Dispose();

				this.Entity.RemoveComponent<BundleDownloaderComponent>();
		}
        /// <summary>
        /// 开始异步对比
        /// </summary>
        /// <returns></returns>
		public async ETTask StartAsync()
		{
			//获取远程的Version.txt
			string versionUrl = "";
			try
			{
				using (UnityWebRequestAsync webRequestAsync = ComponentFactory.Create<UnityWebRequestAsync>())
				{
					versionUrl = GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/Version.txt";
                    await webRequestAsync.DownloadAsync(versionUrl);
					remoteVersionConfig = JsonHelper.FromJson<VersionConfig>(webRequestAsync.Request.downloadHandler.text); 
                }

			}
			catch (Exception e)
			{
				throw new Exception($"url: {versionUrl}", e);
			}

			// 获取streaming目录的Version.txt
			VersionConfig streamingVersionConfig;
			string versionPath = Path.Combine(PathHelper.AppSA, "Version.txt");
			using (UnityWebRequestAsync request = ComponentFactory.Create<UnityWebRequestAsync>())
			{
				await request.DownloadAsync(versionPath);
				streamingVersionConfig = JsonHelper.FromJson<VersionConfig>(request.Request.downloadHandler.text);
			}
            Log.Debug("本地SA游戏版本号：" + streamingVersionConfig.Version);
            Log.Debug("远端游戏版本号：" + remoteVersionConfig.Version);
            //1.比较两个版本号

            //2.更新客户端

            //3.更新完成关闭

            
            //删掉远程不存在的文件
            DirectoryInfo directoryInfo = new DirectoryInfo(PathHelper.AppHotfixResPath);
			if (directoryInfo.Exists)
			{
				FileInfo[] fileInfos = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in fileInfos)
				{
					if (remoteVersionConfig.FileInfoDict.ContainsKey(fileInfo.Name))
					{
						continue;
					}

					if (fileInfo.Name == "Version.txt")
					{
						continue;
					}
					
					fileInfo.Delete();
				}
			}
			else
			{
				directoryInfo.Create();
			}

			//对比MD5
			foreach (FileVersionInfo fileVersionInfo in remoteVersionConfig.FileInfoDict.Values)
			{
                Log.Debug("对比的包："+fileVersionInfo.File);
				//对比md5

				string localFileMD5 = BundleHelper.GetBundleMD5(streamingVersionConfig, fileVersionInfo.File);
                Log.Debug(fileVersionInfo.MD5+"MD5对比：" + localFileMD5);
                if (fileVersionInfo.MD5 == localFileMD5)
				{
					continue;
				}
                //本地
                this.bundles.Enqueue(fileVersionInfo.File);
                
                this.TotalSize += fileVersionInfo.Size;
			}
		}

		public int Progress
		{
			get
			{
				if (this.TotalSize == 0)
				{
                    Log.Debug("无需下载资源");
					return 100;
				}

				long alreadyDownloadBytes = 0;
				foreach (string downloadedBundle in this.downloadedBundles)
				{
					long size = this.remoteVersionConfig.FileInfoDict[downloadedBundle].Size;
					alreadyDownloadBytes += size;
				}
				if (this.webRequest != null)
				{
					alreadyDownloadBytes += (long)this.webRequest.Request.downloadedBytes;
				}
                Log.Debug("我要下载的大小：" + this.TotalSize);
                return (int)(alreadyDownloadBytes * 100f / this.TotalSize);
			}
		}
        /// <summary>
        /// 开始下载
        /// </summary>
        /// <returns></returns>
		public async ETTask DownloadAsync()
		{
            Log.Debug("ab包数量：" + this.bundles.Count+"下载名称："+ downloadingBundle);
			if (this.bundles.Count == 0 && this.downloadingBundle == "")
			{
				return;
			}

			try
			{
				while (true)
				{
					if (this.bundles.Count == 0)
					{
						break;
					}

					this.downloadingBundle = this.bundles.Dequeue();

					while (true)
					{
						try
						{
							using (this.webRequest = ComponentFactory.Create<UnityWebRequestAsync>())
							{
                                //Log.Debug("下载文件：" + GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/" + this.downloadingBundle);
                                await this.webRequest.DownloadAsync(GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/" + this.downloadingBundle);
								byte[] data = this.webRequest.Request.downloadHandler.data;
                                //下载完成 拷贝到本地PD
                                string path = Path.Combine(PathHelper.AppHotfixResPath, this.downloadingBundle);
								using (FileStream fs = new FileStream(path, FileMode.Create))
								{
									fs.Write(data, 0, data.Length);
								}
							}
						}
						catch (Exception e)
						{
							Log.Error($"download bundle error: {this.downloadingBundle}\n{e}");
							continue;
						}

						break;
					}
					this.downloadedBundles.Add(this.downloadingBundle);
					this.downloadingBundle = "";
					this.webRequest = null;
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}
