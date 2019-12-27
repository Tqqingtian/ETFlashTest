using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UIInitGameComponentSystem : AwakeSystem<UIInitGameComponent>
	{
		public override void Awake(UIInitGameComponent self)
		{
			self.Awake();
		}
	}
	
	public class UIInitGameComponent : Component
	{
		private GameObject btn_EnterGame;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            btn_EnterGame = rc.Get<GameObject>("Btn_EnterGame");
            btn_EnterGame.GetComponent<Button>().onClick.Add(this.EnterGame);
           
        }

		private void EnterGame()
		{
            Log.Info("点击 进入游戏 按钮");

            Game.EventSystem.Run(EventIdType.InitGameFinish);
            MainHelper.EnterMainAsync().Coroutine();
        }
	}
}
