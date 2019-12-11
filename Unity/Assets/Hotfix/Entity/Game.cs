using UnityEngine;

namespace ETHotfix
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
	public static class Game
	{
		private static EventSystem eventSystem;
        /// <summary>
        /// 事件管理器
        /// </summary>
		public static EventSystem EventSystem
		{
			get
			{
                //eventSystem 空就 返回(eventSystem = new EventSystem())
                return eventSystem ?? (eventSystem = new EventSystem());
			}
		}
		
		private static Scene scene;
        /// <summary>
        /// 场景管理器
        /// </summary>
		public static Scene Scene
		{
			get
			{
				if (scene != null)
				{
					return scene;
				}
				scene = new Scene() { Name = "ClientH" };
				return scene;
			}
		}

		private static ObjectPool objectPool;

        /// <summary>
        /// 对象池
        /// </summary>
		public static ObjectPool ObjectPool
		{
			get
			{
				if (objectPool != null)
				{
					return objectPool;
				}
				objectPool = new ObjectPool() { Name = "ClientH" };
				return objectPool;
			}
		}
        /// <summary>
        /// 游戏关闭
        /// </summary>
		public static void Close()
		{
			scene?.Dispose();
			scene = null;
			
			objectPool?.Dispose();
			objectPool = null;
			
			eventSystem = null;
		}
	}
}