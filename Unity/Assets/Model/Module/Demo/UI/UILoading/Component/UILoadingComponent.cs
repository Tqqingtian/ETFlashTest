using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
	[ObjectSystem]
	public class UiLoadingComponentAwakeSystem : AwakeSystem<UILoadingComponent>
	{
		public override void Awake(UILoadingComponent self)
		{
            self.text = self.GetParent<UI>().GameObject.Get<GameObject>("Text").GetComponent<Text>();
            self.sdr_Download = self.GetParent<UI>().GameObject.Get<GameObject>("Sder_Download").GetComponent<Slider>();
        }
    }

    [ObjectSystem]
    public class UiLoadingComponentStartSystem : StartSystem<UILoadingComponent>
    {
        public override void Start(UILoadingComponent self)
        {
            StartAsync(self).Coroutine();
        }
        /// <summary>
        /// 开始等待
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public async ETVoid StartAsync(UILoadingComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            long instanceId = self.InstanceId;

            while (true)
            {
                await timerComponent.WaitAsync(10);
                if (self.InstanceId != instanceId)
                {
                    return;
                }

                BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
                if (bundleDownloaderComponent == null)
                {
                    continue;
                }
                self.sdr_Download.value = bundleDownloaderComponent.Progress / 100f;
                self.text.text = $"{bundleDownloaderComponent.Progress}%";
            }
        }
    }

    public class UILoadingComponent : Component
	{
		public Text text;
        public Slider sdr_Download;
	}
}
