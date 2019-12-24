using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
	[ObjectSystem]
	public class UiLoadingComponentAwakeSystem : AwakeSystem<UILoadingComponent>
	{
		public override void Awake(UILoadingComponent self)
		{
            Log.Debug("[UiLoadingComponentStartSystem.Awake()]");
            self.text = self.GetParent<UI>().GameObject.Get<GameObject>("Text").GetComponent<Text>();
		}
	}

	[ObjectSystem]
	public class UiLoadingComponentStartSystem : StartSystem<UILoadingComponent>
	{
		public override void Start(UILoadingComponent self)
		{
            Log.Debug("[UiLoadingComponentStartSystem.Start()]");
            StartAsync(self).Coroutine();
		}
		/// <summary>
        /// 开始等待
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
		public async ETVoid StartAsync(UILoadingComponent self)
		{
            Log.Debug("开始更新的UI显示");
			TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
			long instanceId = self.InstanceId;
            
			while (true)
			{
				await timerComponent.WaitAsync(1000);
                Log.Debug("当前Id：" + instanceId.ToString()+"本生Id："+self.InstanceId);
                if (self.InstanceId != instanceId)
				{
					return;
				}

				BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
				if (bundleDownloaderComponent == null)
				{
					continue;
				}
                Log.Debug("包的大小："+ bundleDownloaderComponent.TotalSize +"包的比例："+ bundleDownloaderComponent.Progress.ToString());
				self.text.text = $"{bundleDownloaderComponent.Progress}%";
			}
		}
	}

	public class UILoadingComponent : Component
	{
		public Text text;
	}
}
