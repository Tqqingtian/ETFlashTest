using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    public static class MainHelper 
    {
        public static async ETVoid EnterMainAsync()
        {
            try
            {
                //1.获取资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                await resourcesComponent.LoadBundleAsync("unit.unity3d");
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("main.unity3d");

                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Main);
                    Log.Debug("场景加载进度："+sceneChangeComponent.Process);
                }

                Game.Scene.AddComponent<MainComponent>();

                Game.EventSystem.Run(EventIdType.EnterMainFinish);
            }
            catch (System.Exception e)
            {
                Log.Error(e);

            }

        }
    }
}

