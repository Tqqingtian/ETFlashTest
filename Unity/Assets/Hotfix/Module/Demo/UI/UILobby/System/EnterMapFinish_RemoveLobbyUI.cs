using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// 进入地图移除大厅UI
    /// </summary>
	[Event(EventIdType.EnterMapFinish)]
	public class EnterMapFinish_RemoveLobbyUI: AEvent
	{
		public override void Run()
		{
            Log.Info("移除大厅UI");
            //移除UI
			Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
            //卸载UI资源
			ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UILobby.StringToAB());
		}
	}
}
