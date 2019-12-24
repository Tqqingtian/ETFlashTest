using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ETHotfix
{
    public class Load1Helpr
    {
        // Start is called before the first frame update
        public static async ETVoid EnterLoad1SceneAsync()
        {
            try
            {
                //1.获取资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("load1.unity3d");

                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Load1);
                }

                Game.EventSystem.Run(EventIdType.EnterLoad1SceneFinish);
            }
            catch (System.Exception e)
            {
                Log.Error(e);

            }

        }
    }

    public class Load2Helpr
    {
        // Start is called before the first frame update
        public static async ETVoid EnterLoad2SceneAsync()
        {
            try
            {
                //1.获取资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("load2.unity3d");

                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Load2);
                }

                //Game.EventSystem.Run(EventIdType.EnterLoad1SceneFinish);
            }
            catch (System.Exception e)
            {
                Log.Error(e);

            }

        }
    }
}
