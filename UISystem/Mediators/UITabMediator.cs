using Cysharp.Threading.Tasks;
using UISystem.History;
using UISystem.UITypes;

namespace UISystem.Mediators
{
	public class UITabMediator
	{
		readonly UITabFactory _uiTabFactory;
		readonly IUIHistory _uiHistory;
		public readonly UIMediator UiMediator;
		
		public UITabMediator(UITabFactory uiTabFactory, IUIHistory uiHistory, UIMediator uiMediator)
		{
			_uiTabFactory = uiTabFactory;
			_uiHistory = uiHistory;
			UiMediator = uiMediator;
		}

		public async UniTask<UIPage> Add(UIPage entity) => await _uiTabFactory.Create<UIPage>(entity);
		public async void Back() => await _uiHistory.Back();
		public async void Next() => await _uiHistory.Next();
		public async void Set(int pageIndex) => await _uiHistory.Set(pageIndex);
		public async void CloseCurrentPage() => await _uiHistory.CloseCurrentPage();
		public bool HasRequestToHistory() => _uiHistory.HasRequestToHistory();
		public int PagesCount => _uiHistory.PagesCount;
	}
}