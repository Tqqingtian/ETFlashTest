namespace ETHotfix
{
    /// <summary>
    /// 对象 助手
    /// </summary>
	public static class ObjectHelper
	{
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">要换的对象</param>
        /// <param name="t2">被换的对象</param>
		public static void Swap<T>(ref T t1, ref T t2)
		{
			T t3 = t1;
			t1 = t2;
			t2 = t3;
		}
	}
}