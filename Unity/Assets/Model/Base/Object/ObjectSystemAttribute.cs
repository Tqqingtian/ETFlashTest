using System;

namespace ETModel
{
    /// <summary>
    /// 物体特性（如果是物体就添加这个特性）
    /// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ObjectSystemAttribute: BaseAttribute
	{
	}
}