using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
	public class Init : MonoBehaviour
	{
		private void Start()
		{
			this.StartAsync().Coroutine();
		}
		
		private async ETVoid StartAsync()
		{
			try
			{
                Log.Debug(PathHelper.AppHotfixResPath);
				SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
                //DontDestroyOnLoad(gameObject);
                Game.EventSystem.Add(DLLType.Model, typeof(Init).Assembly);

				Game.Scene.AddComponent<TimerComponent>();//计时器
				Game.Scene.AddComponent<GlobalConfigComponent>();
				Game.Scene.AddComponent<NetOuterComponent>();
				Game.Scene.AddComponent<ResourcesComponent>();
				Game.Scene.AddComponent<PlayerComponent>();
				Game.Scene.AddComponent<UnitComponent>();//
				Game.Scene.AddComponent<UIComponent>();//UI
                // 下载ab包
                Log.Debug("下载assetebundle开始");
				await BundleHelper.DownloadBundle();
				Game.Hotfix.LoadHotfixAssembly();
				// 加载配置
				Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
				Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatcherComponent>();

				Game.Hotfix.GotoHotfix();

				Game.EventSystem.Run<string>(EventIdType.TestHotfixSubscribMonoEvent, "TestHotfixSubscribMonoEvent");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private void Update()
		{
			OneThreadSynchronizationContext.Instance.Update();
            //添加进入更新
			Game.Hotfix.Update?.Invoke();

			Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
            //
            Game.Hotfix.LateUpdate?.Invoke();
			Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
            //程序退出
			Game.Hotfix.OnApplicationQuit?.Invoke();
			Game.Close();
		}
	}
}