using Cysharp.Threading.Tasks;
using UISystem.History;
using UISystem.Mediators;
using UISystem.Providers;
using UISystem.UITypes;
using UnityEngine;

namespace UISystem
{
	public class UITabFactory
	{
		Transform _parent;
		UIHistory _history = new UIHistory();
		protected UITabMediator UITabMediator;
		protected IUIProvider UiProvider;

		public UITabFactory(IUIProvider uiProvider, Transform parent = null, UIMediator uiMediator = null)
		{
			_parent = parent;
			UiProvider = uiProvider ?? new UIProvider();
			UITabMediator = new UITabMediator(this, _history, uiMediator);
		}

		protected void InitProtected(Transform parent, UIHistory history)
		{
			_parent = parent;
			_history = history;
		}

		public async UniTask<T> Create<T>(object entityPrefab, object customData = null) where T : UIEntity
		{
			var entity = await UiProvider.InstantiatePrefabInactiveAsync<T>(entityPrefab);
			
			var entityTransform = entity.transform;
			entityTransform.SetParent(_parent);
			entityTransform.localPosition = Vector3.zero;
			entityTransform.localRotation = Quaternion.identity;

			if (entity is UIPage uiPage)
			{
				uiPage.InitUITabMediator(UITabMediator);
				await _history.Add(uiPage);
			}

			entity.Init(customData);

			return entity;
		}
	}
}