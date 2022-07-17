using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UISystem.Extensions;
using UISystem.UITypes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UISystem.History
{
	[Serializable]
	public class UIHistory : IUIHistory
	{
#region Serialized fields

		[SerializeField] List<UIPage> _pages = new List<UIPage>();
		[SerializeField] int _currentPageIndex = -1;

#endregion

#region Private fields

		readonly SemaphoreSlim _historySemaphore = new SemaphoreSlim(1, 1);

#endregion

#region Public fields

		public UIPage this[int i] => _pages[i];
		public int PagesCount => _pages.Count;
		public int CurrentPageIndex => _currentPageIndex;

#endregion

#region Public Methods

		public async UniTask Add(UIPage uiPage)
		{
			using (await _historySemaphore.UseWaitAsync())
			{
				DestroyNextPages();

				_pages.Add(uiPage);

				await NextLogic();
			}
		}

		public async UniTask Back()
		{
			using (await _historySemaphore.UseWaitAsync())
				await BackLogic();
		}

		public async UniTask Next()
		{
			using (await _historySemaphore.UseWaitAsync())
				await NextLogic();
		}

		public async UniTask Set(int pageIndex)
		{
			using (await _historySemaphore.UseWaitAsync())
				await SetLogic(pageIndex);
		}

		public async UniTask CloseCurrentPage()
		{
			using (await _historySemaphore.UseWaitAsync())
				await ClosePage(_currentPageIndex);
		}

		public bool HasRequestToHistory()
		{
			return _historySemaphore.CurrentCount <= 0;
		}

		public UIPage GetCurrentPage()
		{
			if (_pages.Count > 0 && _currentPageIndex > -1 && _currentPageIndex < _pages.Count)
				return _pages[_currentPageIndex];

			return null;
		}

		public async UniTask CloseAllPages()
		{
			await _pages[_currentPageIndex].Hide();

			foreach (var uiPage in _pages)
				Object.Destroy(uiPage.gameObject);

			_pages.Clear();
			_currentPageIndex = -1;
		}

#endregion

#region Private Methods

		async UniTask BackLogic()
		{
			if (_currentPageIndex < 1) return;

			await ClosePage(_currentPageIndex);

			_currentPageIndex--;
			await ShowPage(_currentPageIndex);
		}

		async UniTask NextLogic()
		{
			if (_currentPageIndex >= _pages.Count - 1) return;

			if (_pages.Count > 1)
				await ClosePage(_currentPageIndex);

			_currentPageIndex++;
			await ShowPage(_currentPageIndex);
		}

		async UniTask SetLogic(int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= PagesCount) return;

			if (_currentPageIndex != pageIndex)
				await ClosePage(_currentPageIndex);

			_currentPageIndex = pageIndex;
			await ShowPage(_currentPageIndex);
		}

		async UniTask ShowPage(int pageIdx) => await _pages[pageIdx].Show();

		async UniTask ClosePage(int pageIdx) => await _pages[pageIdx].Hide();

		void DestroyNextPages()
		{
			if (_currentPageIndex < 0 || _currentPageIndex == _pages.Count - 1) return;

			var nextPages = _pages.GetRange(_currentPageIndex + 1, _pages.Count - _currentPageIndex - 1);

			foreach (var page in nextPages)
				Object.Destroy(page.gameObject);

			_pages.RemoveRange(_currentPageIndex + 1, _pages.Count - _currentPageIndex - 1);
		}

#endregion
	}
}