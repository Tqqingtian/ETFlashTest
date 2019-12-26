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
            //StartAsync(self).Coroutine();
        }


        //public async ETVoid StartAsync(UILoadingComponent self)
        //{
        //    Log.Debug("开始更新的UI显示");
        //    TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
        //    long instanceId = self.InstanceId;
        //    while (true)
        //    {
        //        Log.Debug("当前==Id：" + instanceId + "=> 对比 自身==Id：" + self.InstanceId);
        //        await timerComponent.WaitAsync(10);
        //        Log.Debug("当前Id：" + instanceId + "=> 对比 自身Id：" + self.InstanceId);
        //        if (self.InstanceId != instanceId)
        //        {
        //            return;
        //        }

        //        BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
        //        if (bundleDownloaderComponent == null)
        //        {
        //            continue;
        //        }
        //        Log.Debug("包的大小：" + bundleDownloaderComponent.TotalSize + "包的比例：" + bundleDownloaderComponent.Progress.ToString());
        //        self.text.text = $"{bundleDownloaderComponent.Progress}%";
        //    }
        //}
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
                await timerComponent.WaitAsync(10);
                Log.Debug("当前Id：" + instanceId.ToString() + "本生Id：" + self.InstanceId);
                if (self.InstanceId != instanceId)
                {
                    return;
                }

                BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
                if (bundleDownloaderComponent == null)
                {
                    continue;
                }
                Log.Debug("包的大小：" + bundleDownloaderComponent.TotalSize + "包的比例：" + bundleDownloaderComponent.Progress.ToString());
                self.text.text = $"{bundleDownloaderComponent.Progress}%";
            }
        }
    }

    public class UILoadingComponent : Component
	{
		public Text text;
	}
}
