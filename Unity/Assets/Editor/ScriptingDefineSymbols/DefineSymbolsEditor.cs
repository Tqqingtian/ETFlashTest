using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ETEditor
{
    /// <summary>
    /// 宏设置
    /// </summary>
    public class DefineSymbolsEditor : EditorWindow
    {
        [MenuItem("Tools/设置宏")]
        public static void ShowWindow()
        {
            GetWindow(typeof(DefineSymbolsEditor));
        }


        private List<MacorItem> m_List = new List<MacorItem>();
        private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
        private string m_Macor = null;

        void OnEnable()
        {
            m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
            m_List.Clear();
            m_List.Add(new MacorItem() { Name = "NET452", DisplayName = ".NET代码", IsDebug = true, IsRelease = true });
            m_List.Add(new MacorItem() { Name = "ILRuntime", DisplayName = "ILRuntime模式", IsDebug = false, IsRelease = false });
            m_List.Add(new MacorItem() { Name = "IsOnlyScene", DisplayName = "单场景", IsDebug = false, IsRelease = true });

            for (int i = 0; i < m_List.Count; i++)
            {
                if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
                {
                    m_Dic[m_List[i].Name] = true;
                }
                else
                {
                    m_Dic[m_List[i].Name] = false;
                }
            }
        }


        void OnGUI()
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                EditorGUILayout.BeginHorizontal("box");
                m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
                EditorGUILayout.EndHorizontal();
            }

            //开启一行
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存", GUILayout.Width(100)))
            {
                SaveMacor();
            }

            if (GUILayout.Button("调试模式", GUILayout.Width(100)))
            {
                for (int i = 0; i < m_List.Count; i++)
                {
                    m_Dic[m_List[i].Name] = m_List[i].IsDebug;
                }
                SaveMacor();
            }

            if (GUILayout.Button("发布模式", GUILayout.Width(100)))
            {
                for (int i = 0; i < m_List.Count; i++)
                {
                    m_Dic[m_List[i].Name] = m_List[i].IsRelease;
                }
                SaveMacor();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SaveMacor()
        {
            m_Macor = string.Empty;
            foreach (var item in m_Dic)
            {
                if (item.Value)
                {
                    m_Macor += string.Format("{0};", item.Key);
                }

                if (item.Key.Equals("IsOnlyScene", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    EditorBuildSettingsScene[] arrScene = EditorBuildSettings.scenes;
                    for (int i = 0; i < arrScene.Length; i++)
                    {
                        if (arrScene[i].path != "Assets/Scenes/Init.unity")
                        {
                            arrScene[i].enabled = !item.Value;
                        }
                    }
                    EditorBuildSettings.scenes = arrScene;
                }
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
        }

        /// <summary>
        /// 宏项目
        /// </summary>
        public class MacorItem
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name;

            /// <summary>
            /// 显示的名称
            /// </summary>
            public string DisplayName;

            /// <summary>
            /// 是否调试项（暂时不用）
            /// </summary>
            public bool IsDebug;

            /// <summary>
            /// 是否发布项
            /// </summary>
            public bool IsRelease;
        }
    }

}
