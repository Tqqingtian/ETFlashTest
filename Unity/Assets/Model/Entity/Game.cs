using UnityEngine;

namespace ETModel
{
	public static class Game
	{
		private static EventSystem eventSystem;

		public static EventSystem EventSystem
		{
			get
			{
				return eventSystem ?? (eventSystem = new EventSystem());
			}
		}
		
		private static Scene scene;

		public static Scene Scene
		{
			get
			{
				if (scene != null)
				{
					return scene;
				}
				scene = new Scene() { Name = "ClientM" };
				return scene;
			}
		}

		private static ObjectPool objectPool;

		public static ObjectPool ObjectPool
		{
			get
			{
				if (objectPool != null)
				{
					return objectPool;
				}
				objectPool = new ObjectPool() { Name = "ClientM" };
				return objectPool;
			}
		}

		private static Hotfix hotfix;

		public static Hotfix Hotfix
		{
			get
			{
                //a??b 当a为null时则返回b，a不为null时则返回a本身。
                return hotfix ?? (hotfix = new Hotfix());
			}
		}

		public static void Close()
		{
            //不等空就执行
			scene?.Dispose();
			scene = null;
			
			objectPool?.Dispose();
			objectPool = null;
			
			hotfix = null;
			
			eventSystem = null;
		}
	}
}