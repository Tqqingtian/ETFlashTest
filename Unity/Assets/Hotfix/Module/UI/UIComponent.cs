using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    /// <summary>
    /// ui的开始组件系统
    /// </summary>
	[ObjectSystem]
	public class UIComponentAwakeSystem : AwakeSystem<UIComponent>
	{
		public override void Awake(UIComponent self)
		{
            //获取UI眼镜
			self.Camera = Component.Global.transform.Find("UICamera").gameObject;
		}
	}
	
	/// <summary>
	/// 管理组件
	/// </summary>
	public class UIComponent: Component
	{
        /// <summary>
        /// UI眼镜
        /// </summary>
		public GameObject Camera;
		/// <summary>
        /// ui的字典
        /// </summary>
		public Dictionary<string, UI> uis = new Dictionary<string, UI>();

        /// <summary>
        /// 添加UI
        /// </summary>
        /// <param name="ui"></param>
		public void Add(UI ui)
		{
			ui.GameObject.GetComponent<Canvas>().worldCamera = this.Camera.GetComponent<Camera>();
			this.uis.Add(ui.Name, ui);
			ui.Parent = this;
		}
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="name">UI名称</param>
		public void Remove(string name)
		{
			if (!this.uis.TryGetValue(name, out UI ui))
			{
				return;
			}
			this.uis.Remove(name);
			ui.Dispose();
		}
		/// <summary>
        /// 获取
        /// </summary>
        /// <param name="name">UI名称</param>
        /// <returns></returns>
		public UI Get(string name)
		{
			UI ui = null;
			this.uis.TryGetValue(name, out ui);
			return ui;
		}
	}
}