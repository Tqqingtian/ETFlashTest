#if !SERVER
using UnityEngine;
#endif

namespace ETModel
{
	/// <summary>
	/// 每个Config的组件 如果不是服务器端 就显示到hierarchy面板
	/// </summary>
#if !SERVER
	[HideInHierarchy]
#endif
	public abstract class AConfigComponent: Component, ISerializeToEntity
	{
	}
}