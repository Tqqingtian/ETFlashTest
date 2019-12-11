using ETModel;

namespace ETHotfix
{
	[Config((int)(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map))]
	public partial class UnitConfigCategory : ACategory<UnitConfig>
	{
	}
    /// <summary>
    /// ²¿¼þ¿ØÖÆ
    /// </summary>
	public class UnitConfig: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public int Position;
		public int Height;
		public int Weight;
	}
}
