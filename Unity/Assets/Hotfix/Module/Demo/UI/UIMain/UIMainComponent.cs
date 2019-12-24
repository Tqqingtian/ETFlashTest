using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;
using System;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIMainComponentSystem : AwakeSystem<UIMainComponent>
    {
        public override void Awake(UIMainComponent self)
        {
            self.Awake();
        }
    }

    public class UIMainComponent : Component
    {
        private GameObject pan_Warring;

        private GameObject btn_StartModel;

        private GameObject btn_EndModel;

        private GameObject btn_Load1;

        private GameObject btn_Load2;

        public void Awake()
        {
           
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            Log.Debug("游戏UI主场景");
            pan_Warring = rc.Get<GameObject>("Pan_Warring");
            btn_EndModel = rc.Get<GameObject>("Btn_EndModel");
            btn_StartModel = rc.Get<GameObject>("Btn_StartModel");
            btn_Load1 = rc.Get<GameObject>("Btn_Load1");
            btn_Load2 = rc.Get<GameObject>("Btn_Load2");

            btn_StartModel.GetComponent<Button>().onClick.Add(() => pan_Warring.SetActive(true));
            btn_EndModel.GetComponent<Button>().onClick.Add(() => pan_Warring.SetActive(false));
            Log.Debug("添加加载场景1的监听");
            btn_Load1.GetComponent<Button>().onClick.Add(Load1Scene);

            Log.Debug("添加加载场景1的监听");
            btn_Load2.GetComponent<Button>().onClick.Add(Load2Scene);
            pan_Warring.SetActive(false);

        }
        /// <summary>
        /// 加载场景一
        /// </summary>
        private void Load1Scene()
        {
            Log.Debug("加载场景一");
            Load1Helpr.EnterLoad1SceneAsync().Coroutine();
            
        }
        /// <summary>
        /// 加载场景二
        /// </summary>
        private void Load2Scene()
        {
            Log.Debug("加载场景二");
            Load2Helpr.EnterLoad2SceneAsync().Coroutine();

        }
    }


}

