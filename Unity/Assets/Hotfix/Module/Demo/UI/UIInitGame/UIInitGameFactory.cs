using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    /// <summary>
    /// 创建初始化游戏UI
    /// </summary>
    /// <returns></returns>
    public static class UIInitGameFactory
    {
        public static UI Create()
        {
	        try
	        {
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
		        resourcesComponent.LoadBundle(UIType.UIInitGame.StringToAB());
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UIInitGame.StringToAB(), UIType.UIInitGame);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
		        UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UIInitGame, gameObject, false);

				ui.AddComponent<UIInitGameComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
				Log.Error(e);
		        return null;
	        }
		}
    }

    [Event(EventIdType.InitGameStart)]
    public class InitGameFinish_CreateInitUI : AEvent
    {
        public override void Run()
        {
            UI ui = UIInitGameFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    /// <summary>
    /// 进入地图移除大厅UI
    /// </summary>
    [Event(EventIdType.InitGameFinish)]
    public class InitGameFinish_RemoveInitGameUI : AEvent
    {
        public override void Run()
        {
            //移除UI
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIInitGame);
            //卸载UI资源
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIInitGame.StringToAB());
        }
    }

}