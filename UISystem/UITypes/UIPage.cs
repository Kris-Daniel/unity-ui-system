using Cysharp.Threading.Tasks;
using UISystem.Mediators;
using UnityEngine;

namespace UISystem.UITypes
{
	public class UIPage : UIEntity
	{
		protected UITabMediator UITabMediator { get; private set; }

		public void InitUITabMediator(UITabMediator uiTabMediator) => UITabMediator = uiTabMediator;
		public virtual async UniTask Show() => gameObject.SetActive(true);
		public virtual async UniTask Hide() => gameObject.SetActive(false);
	}
}