using System;
using UISystem.History;
using UISystem.Mediators;
using UISystem.Providers;
using UnityEngine;

namespace UISystem
{
	[Serializable]
	public class UITabFactorySerializable : UITabFactory
	{
		[SerializeField] Transform _parent;
		[SerializeField] UIMediator _uiMediator;
		[SerializeField] UIHistory _history;

		public UITabFactorySerializable(IUIProvider uiProvider, Transform parent = null,
			UIMediator uiMediator = null) : base(uiProvider, parent, uiMediator) { }

		public void Init(IUIProvider uiProvider = null)
		{
			InitProtected(_parent, _history);
			
			UiProvider = uiProvider ?? new UIProvider();
			UITabMediator = new UITabMediator(this, _history, _uiMediator);

			for (int i = 0; i < _history.PagesCount; i++)
			{
				_history[i].InitUITabMediator(UITabMediator);
			}
		}
	}
}