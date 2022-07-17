using Cysharp.Threading.Tasks;
using UISystem.UITypes;
using UnityEngine;

namespace UISystem.Providers
{
    public class UIProvider : IUIProvider
    {
        public async UniTask<T> InstantiatePrefabInactiveAsync<T>(object prefab) where T : UIEntity
        {
            var go = Object.Instantiate( (T) prefab);
            go.gameObject.SetActive(false);
            
            var taskSource = new UniTaskCompletionSource<T>();
            taskSource.TrySetResult(go);
            return await taskSource.Task;
        }
    }
}