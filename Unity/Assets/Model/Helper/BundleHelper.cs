using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
	public static class BundleHelper
	{
        /// <summary>
        /// 下载资源信息(编辑器模式下不会运行)
        /// </summary>
        /// <returns></returns>
		public static async ETTask DownloadBundle()
		{
            if (Define.IsAsync)
			{
				try
				{
					using (BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.AddComponent<BundleDownloaderComponent>())
					{
                        
                        await bundleDownloaderComponent.StartAsync();
                        Log.Debug("运行 登录开始");
						Game.EventSystem.Run(EventIdType.LoadingBegin);
						
						await bundleDownloaderComponent.DownloadAsync();
					}
					Game.EventSystem.Run(EventIdType.LoadingFinish);
					
					Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle("StreamingAssets");
                
                    ResourcesComponent.AssetBundleManifestObject = (AssetBundleManifest)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("StreamingAssets", "AssetBundleManifest");
                  
                }
				catch (Exception e)
				{
					Log.Error(e);
				}

			}
		}
        /// <summary>
        /// 获取bundle的md5
        /// </summary>
        /// <param name="streamingVersionConfig">配置实体</param>
        /// <param name="bundleName">bundle名称</param>
        /// <returns></returns>
		public static string GetBundleMD5(VersionConfig streamingVersionConfig, string bundleName)
		{
			string path = Path.Combine(PathHelper.AppHotfixResPath, bundleName);
			if (File.Exists(path))
			{
				return MD5Helper.FileMD5(path);
			}
			
			if (streamingVersionConfig.FileInfoDict.ContainsKey(bundleName))
			{
				return streamingVersionConfig.FileInfoDict[bundleName].MD5;	
			}

			return "";
		}
	}
}
