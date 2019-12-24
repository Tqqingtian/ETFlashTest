using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ETHotfix
{

    public class UILoad1ComponentSystem : AwakeSystem<UILoad1Component>
    {
        public override void Awake(UILoad1Component self)
        {
            self.Awake();
        }
    }


    public class UILoad1Component : Component
    {

        public void Awake()
        {
            
        }
    }

}

