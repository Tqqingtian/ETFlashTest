﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ETModel
{
	public static class FileHelper
	{
		public static void GetAllFiles(List<string> files, string dir)
		{
			string[] fls = Directory.GetFiles(dir);
			foreach (string fl in fls)
			{
				files.Add(fl);
			}

			string[] subDirs = Directory.GetDirectories(dir);
			foreach (string subDir in subDirs)
			{
				GetAllFiles(files, subDir);
			}
		}
		/// <summary>
        /// 清理目标文件夹(修改)
        /// </summary>
        /// <param name="dir"></param>
		public static void CleanDirectory(string dir)
		{
            if (Directory.Exists(dir))
            {
                foreach (string subdir in Directory.GetDirectories(dir))
                {
                    Directory.Delete(subdir, true);
                }
                foreach (string subFile in Directory.GetFiles(dir))
                {
                    File.Delete(subFile);
                }
            }
            else
            {
                Directory.CreateDirectory(dir);
            }
		}
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="srcDir"></param>
        /// <param name="tgtDir"></param>
		public static void CopyDirectory(string srcDir, string tgtDir)
		{
			DirectoryInfo source = new DirectoryInfo(srcDir);
			DirectoryInfo target = new DirectoryInfo(tgtDir);
	
			if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
			{
				throw new Exception("父目录不能拷贝到子目录！");
			}
	
			if (!source.Exists)
			{
				return;
			}
	
			if (!target.Exists)
			{
				target.Create();
			}
	
			FileInfo[] files = source.GetFiles();
	
			for (int i = 0; i < files.Length; i++)
			{
				File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
			}
	
			DirectoryInfo[] dirs = source.GetDirectories();
	
			for (int j = 0; j < dirs.Length; j++)
			{
				CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
			}
		}
	}
}
