using System;
using System.IO;
using ETModel;
using UnityEditor;

namespace ETEditor
{
    /// <summary>
    /// 启动（将Hotfix拷到）
    /// </summary>
    [InitializeOnLoad]
    public class Startup
    {
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
        private const string CodeDir = "Assets/Res/Code/";
        private const string HotfixDll = "Unity.Hotfix.dll";
        private const string HotfixPdb = "Unity.Hotfix.pdb";

        static Startup()
        {
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "Hotfix.dll.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "Hotfix.pdb.bytes"), true);
            UnityEngine.Debug.Log(Path.Combine(ScriptAssembliesDir, HotfixDll) +"拷贝到=》"+ Path.Combine(CodeDir, "Hotfix.dll.bytes"));
            Log.Info($"复制Hotfix.dll, Hotfix.pdb到Res/Code完成");
            AssetDatabase.Refresh();
        }
    }
}