using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
		{
			UI ui = UILoginFactory.Create(); //创建UI
            Game.Scene.GetComponent<UIComponent>().Add(ui);//添加到场景
		}
	}
}
