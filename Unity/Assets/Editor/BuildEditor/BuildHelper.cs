using System.IO;
using ETModel;
using UnityEditor;

namespace ETEditor
{
	public static class BuildHelper
	{
		private const string relativeDirPrefix = "../Release";

		public static string BuildFolder = "../Release/{0}/StreamingAssets/";
		
		
		[MenuItem("Tools/启动web资源服务器")]
		public static void OpenFileServer()
		{
			ProcessHelper.Run("dotnet", "FileServer.dll", "../FileServer/");
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="buildAssetBundleOptions"></param>
        /// <param name="buildOptions">打包配置</param>
        /// <param name="isBuildExe">是否打包EXE</param>
        /// <param name="isContainAB">是否包涵AB包</param>
		public static void Build(PlatformType type, BuildAssetBundleOptions buildAssetBundleOptions, BuildOptions buildOptions, bool isBuildExe, bool isContainAB)
		{
			BuildTarget buildTarget = BuildTarget.StandaloneWindows;
			string exeName = "ET";
			switch (type)
			{
				case PlatformType.PC:
					buildTarget = BuildTarget.StandaloneWindows64;
					exeName += ".exe";
					break;
				case PlatformType.Android:
					buildTarget = BuildTarget.Android;
					exeName += ".apk";
					break;
				case PlatformType.IOS:
					buildTarget = BuildTarget.iOS;
					break;
				case PlatformType.MacOS:
					buildTarget = BuildTarget.StandaloneOSX;
					break;
			}

			string fold = string.Format(BuildFolder, type);
			if (!Directory.Exists(fold))
			{
				Directory.CreateDirectory(fold);
			}

			
			Log.Debug("开始资源打包:"+ fold);
			BuildPipeline.BuildAssetBundles(fold, buildAssetBundleOptions, buildTarget);
			//创建版本文件信息
			GenerateVersionInfo(fold);
			if (isContainAB)
			{
                Log.Debug("清空SA");
                FileHelper.CleanDirectory("Assets/StreamingAssets/");
                Log.Debug("将资源拷贝到SA");
                FileHelper.CopyDirectory(fold, "Assets/StreamingAssets/");
			}

			if (isBuildExe)
			{
				AssetDatabase.Refresh();
				string[] levels = {
					"Assets/Scenes/Init.unity",
				};
				BuildPipeline.BuildPlayer(levels, $"{relativeDirPrefix}/{exeName}", buildTarget, buildOptions);
				Log.Debug("完成exe打包");
			}
		}
        /// <summary>
        /// 创建版本信息
        /// </summary>
        /// <param name="dir"></param>
		private static void GenerateVersionInfo(string dir)
        {
            VersionConfig versionProto = new VersionConfig();
            Log.Debug(JsonHelper.ToJson(versionProto));

            GenerateVersionProto(dir, versionProto, "");
            using (FileStream fileStream = new FileStream($"{dir}/Version.txt", FileMode.Create))
            {
                byte[] bytes = JsonHelper.ToJson(versionProto).ToByteArray();
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

		private static void GenerateVersionProto(string dir, VersionConfig versionProto, string relativePath)
		{
			foreach (string file in Directory.GetFiles(dir))
			{
				string md5 = MD5Helper.FileMD5(file);
				FileInfo fi = new FileInfo(file);
				long size = fi.Length;
				string filePath = relativePath == "" ? fi.Name : $"{relativePath}/{fi.Name}";

				versionProto.FileInfoDict.Add(filePath, new FileVersionInfo
				{
					File = filePath,
					MD5 = md5,
					Size = size,
				});
			}

			foreach (string directory in Directory.GetDirectories(dir))
			{
				DirectoryInfo dinfo = new DirectoryInfo(directory);
				string rel = relativePath == "" ? dinfo.Name : $"{relativePath}/{dinfo.Name}";
				GenerateVersionProto($"{dir}/{dinfo.Name}", versionProto, rel);
			}
		}
	}
}
