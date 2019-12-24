using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

/// <summary>
/// A console to display Unity's debug logs in-game.
/// </summary>
class ULog : MonoBehaviour
{
    private struct LogData
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }

    private readonly List<LogData> logs = new List<LogData>();


    private Vector2 scrollPosition;
    
    /// <summary>
    /// 是否显示GUI界面
    /// </summary>
    public bool visible = true;

    private bool collapse;

    private static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.green },
            { LogType.Warning, Color.yellow },
        };

    private const string windowTitle = "Console";
    private const float margin = 20;
    private const float width = 700;
    private static readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
    private static readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");

    private readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);
    private Rect windowRect = new Rect(margin, margin, Screen.width - 40, Screen.height - 80);

    void OnEnable()
    {
#if UNITY_5||UNITY_2018
        Application.logMessageReceived += HandleLog;
#else
        Application.RegisterLogCallback(HandleLog);
#endif
    }

    void OnDisable()
    {
#if UNITY_5||UNITY_2018
        Application.logMessageReceived -= HandleLog;
#else
        Application.RegisterLogCallback(null);
#endif

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            logs.Clear();
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 60, Screen.height - 60, 50, 50), "Show"))
        {
            visible = !visible;
        }


        if (!visible)
        {
            return;
        }

        windowRect = GUILayout.Window(123456, windowRect, 
            (int windowID)=> 
            {
                DrawLogsList();
                DrawToolbar();
                GUI.DragWindow(titleBarRect);
            }, 
            windowTitle);
        //GUI.TextArea(new Rect(width + margin + 2, margin, 300, (Screen.height - (margin * 2))/2), $"<数据框> 固定");
        //GUI.TextArea(new Rect(width + margin + 2+ 300, margin, 300, (Screen.height - (margin * 2))/2), "<数据框> 附页");
        //GUI.TextArea(new Rect(width + margin + 2, margin + (Screen.height - (margin * 2)) / 2, 300, (Screen.height - (margin * 2))/2), "<数据框> 临时");
    }
    
    /// <summary>
    /// 删除多余的信息
    /// </summary>
    void DrawLogsList()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        //GUILayout.BeginScrollView(scrollPosition);

        for (var i = 0; i < logs.Count; i++)
        {
            LogData log = logs[i];

            // Combine identical messages if collapse option is chosen.
            if (collapse && i > 0)
            {
                var previousMessage = logs[i - 1].message;

                if (log.message == previousMessage)
                {
                    continue;
                }
            }
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.background = null;    //设置背景填充
            fontStyle.normal.textColor = logTypeColors[log.type];   //设置字体颜色
            fontStyle.fontSize = 22;       //字体大小
            GUILayout.Label(log.message, fontStyle);
        }

        GUILayout.EndScrollView();

        // Ensure GUI colour is reset before drawing other components.
        GUI.contentColor = Color.white;
       
    }

    /// <summary>
    /// 显示筛选和更改日志列表的选项。
    /// </summary>
    void DrawToolbar()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(clearLabel))
        {
            logs.Clear();
        }

        collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));

        GUILayout.EndHorizontal();
    }


    /// <summary>
    /// 接收信息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    void HandleLog(string message, string stackTrace, LogType type)
    {
        logs.Add(new LogData
        {
            message = message,
            stackTrace = stackTrace,
            type = type,
        });

        int amountToRemove = Mathf.Max(logs.Count - 1000, 0);
        if (amountToRemove == 0)  return;
        logs.RemoveRange(0, amountToRemove);
    }

    /// <summary>
    /// 一般
    /// </summary>
    /// <param name="massege"></param>
    public static void Log(object massege)
    {
        Debug.Log(massege);
    }

    /// <summary>
    /// 警告
    /// </summary>
    /// <param name="massege"></param>
    public static void RedLog(object massege)
    {
        MonoBehaviour.print("<color=#FF0000>" + massege.ToString() + "</color>");
    }

    /// <summary>
    /// 次要
    /// </summary>
    /// <param name="massege"></param>
    public static void BlueLog(object massege)
    {
        MonoBehaviour.print("<color=#0000FF>" + massege.ToString() + "</color>");
    }

    /// <summary>
    /// 粉色
    /// </summary>
    /// <param name="massege"></param>
    public static void PinkLog(object massege)
    {
        MonoBehaviour.print("<color=#FF00FF>" + massege.ToString() + "</color>");
    }

    /// <summary>
    /// 橙色
    /// </summary>
    /// <param name="massege"></param>
    public static void OrangeLog(object massege)
    {
        MonoBehaviour.print("<color=#FF7F00>" + massege.ToString() + "</color>");
    }
}
