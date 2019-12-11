using ETModel;

namespace ETHotfix
{
	public sealed class Scene: Entity
	{
        /// <summary>
        /// 名称
        /// </summary>
		public string Name { get; set; }
        /// <summary>
        /// 场景
        /// </summary>
		public Scene()
		{
		}

		public Scene(long id): base(id)
		{
		}
        /// <summary>
        /// 卸载
        /// </summary>
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
		}
	}
}