using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 单位
    /// </summary>
    public static class UnitFactory
    {
        /// <summary>
        /// 创建单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Unit Create(long id)
        {
            Log.Info("创建角色："+id);
	        ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
	        GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");
	        GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");
	        
            UnitComponent unitComponent = Game.Scene.GetComponent<UnitComponent>();
            
	        GameObject go = UnityEngine.Object.Instantiate(prefab);
	        Unit unit = ComponentFactory.CreateWithId<Unit, GameObject>(id, go);
	        
			unit.AddComponent<AnimatorComponent>();
	        unit.AddComponent<MoveComponent>();
	        unit.AddComponent<TurnComponent>();
	        unit.AddComponent<UnitPathComponent>();

            unitComponent.Add(unit);
            return unit;
        }
    }
}