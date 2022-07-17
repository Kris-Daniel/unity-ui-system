using Cysharp.Threading.Tasks;
using UISystem.UITypes;

namespace UISystem.History
{
	public interface IUIHistory
	{
		UniTask Add(UIPage uiPage);
		UniTask Back();
		UniTask Next();
		UniTask Set(int pageIndex);
		UniTask CloseCurrentPage();
		bool HasRequestToHistory();
		int PagesCount { get; }
	}
}