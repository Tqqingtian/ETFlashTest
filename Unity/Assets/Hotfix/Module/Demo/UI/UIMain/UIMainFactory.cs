using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;

namespace ETHotfix
{
    public static class UIMainFactory
    {
        public static UI Create()
        {
            try
            {
                string uiType = UIType.UIMain;
                //1.获取资源组件
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                //加载bundle
                resourcesComponent.LoadBundle(uiType.StringToAB());
                //将bundle加载成obj
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(uiType.StringToAB(), uiType);
                //实例化
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
                UI ui = ComponentFactory.Create<UI, string, GameObject>(uiType, gameObject, false);

                ui.AddComponent<UIMainComponent>();
                return ui;
            }
            catch (System.Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
    /// <summary>
    /// 进入游戏主场景
    /// </summary>
    [Event(EventIdType.EnterMainFinish)]
    public class MainFinish_CreateMainUI : AEvent
    {
        public override void Run()
        {
            UI ui = UIMainFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    /// <summary>
    /// 退出游戏主场景
    /// </summary>
    [Event(EventIdType.RemoveMain)]
    public class RemoveMainFinish_RemoveMainUI : AEvent
    {
        public override void Run()
        {
            //移除UI
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIMain);
            //卸载UI资源
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIMain.StringToAB());
        }
    }
}

