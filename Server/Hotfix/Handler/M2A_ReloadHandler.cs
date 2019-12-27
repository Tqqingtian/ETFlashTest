using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.AllServer)]
	public class M2A_ReloadHandler : AMRpcHandler<M2A_Reload, A2M_Reload>
	{
		protected override async ETTask Run(Session session, M2A_Reload request, A2M_Reload response, Action reply)
		{
            Log.Debug("服务器DLL热更新开始");
			Game.EventSystem.Add(DLLType.Hotfix, DllHelper.GetHotfixAssembly());
            Log.Debug("服务器DLL热更新完成");
            reply();
			await ETTask.CompletedTask;
		}
	}
}