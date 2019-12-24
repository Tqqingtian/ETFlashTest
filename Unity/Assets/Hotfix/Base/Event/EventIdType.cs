namespace ETHotfix
{
    /// <summary>
    /// 事件类型
    /// </summary>
	public static class EventIdType
	{
        /// <summary>
        /// 初始化
        /// </summary>
		public const string InitSceneStart = "InitSceneStart";
        /// <summary>
        /// 登录完成
        /// </summary>
		public const string LoginFinish = "LoginFinish";
        /// <summary>
        /// 进入地图
        /// </summary>
		public const string EnterMapFinish = "EnterMapFinish";

        
        /// <summary>
        /// 初始化游戏
        /// </summary>
        public const string InitGameStart = "InitGameStart";
        /// <summary>
        /// 初始化游戏完成
        /// </summary>
        public const string InitGameFinish = "InitGameFinish";
        /// <summary>
        /// 进入主场景完成
        /// </summary>
        public const string EnterMainFinish = "EnterMainFinish";
        /// <summary>
        /// 移除场景
        /// </summary>
        public const string RemoveMain = "RemoveMain";

        /// <summary>
        /// 进入场景1
        /// </summary>
        public const string EnterLoad1SceneFinish = "EnterLoad1SceneFinish";
    }
}