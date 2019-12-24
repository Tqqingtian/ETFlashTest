using System.ComponentModel;

namespace ETModel
{
    /// <summary>
    /// 对象
    /// </summary>
	public abstract class Object: ISupportInitialize
	{
        /// <summary>
        /// 开始初始化
        /// </summary>
		public virtual void BeginInit()
		{
		}
        /// <summary>
        /// 结束初始化
        /// </summary>
		public virtual void EndInit()
		{
		}
	}
}