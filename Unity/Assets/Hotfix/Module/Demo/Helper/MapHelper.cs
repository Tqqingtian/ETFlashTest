using System;
using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// 地图助手
    /// </summary>
    public static class MapHelper
    {
        /// <summary>
        /// 进入地图
        /// </summary>
        /// <returns></returns>
        public static async ETVoid EnterMapAsync()
        {
            try
            {
                //加载资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                Log.Info("加载资源："+ $"unit.unity3d");
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("map.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Map);
                }
				
                G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap()) as G2C_EnterMap;

                PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;
				
                Game.Scene.AddComponent<OperaComponent>();
				
                Game.EventSystem.Run(EventIdType.EnterMapFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}