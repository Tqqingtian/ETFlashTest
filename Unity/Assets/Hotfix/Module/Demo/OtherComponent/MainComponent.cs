using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class MainComponentAwakeSystem : AwakeSystem<MainComponent>
    {
        public override void Awake(MainComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class MainComponentUpdateSystem : UpdateSystem<MainComponent>
    {
        public override void Update(MainComponent self)
        {
            self.Update();
        }
    }
    /// <summary>
    /// 主场景主件
    /// </summary>
	public class MainComponent : Component
    {

        public void Awake()
        {
            Log.Info("初始化主场景");

        }
        

        public void Update()
        {

            //if (Input.GetMouseButtonDown(1))
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit, 1000, this.mapMask))
            //    {
            //        this.ClickPoint = hit.point;
            //        frameClickMap.X = this.ClickPoint.x;
            //        frameClickMap.Y = this.ClickPoint.y;
            //        frameClickMap.Z = this.ClickPoint.z;
            //        ETModel.SessionComponent.Instance.Session.Send(frameClickMap);

            //        // 测试actor rpc消息
            //        this.TestActor().Coroutine();
            //    }
            //}
        }

      
    }
}
