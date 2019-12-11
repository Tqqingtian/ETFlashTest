using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
#if !SERVER
using UnityEngine;
#endif

namespace ETModel
{
    /// <summary>
    /// 开始配置
    /// </summary>
#if !SERVER
	[HideInHierarchy]
#endif
	public class StartConfig: Entity
	{
		public int AppId { get; set; }

		[BsonRepresentation(BsonType.String)]
		public AppType AppType { get; set; }

		public string ServerIP { get; set; }
	}
}