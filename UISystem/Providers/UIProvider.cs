using Cysharp.Threading.Tasks;
using UISystem.UITypes;
using UnityEngine;

namespace UISystem.Providers
{
    public class UIProvider : IUIProvider
    {
        public async UniTask<T> InstantiatePrefabInactiveAsync<T>(object prefab) where T : UIEntity
        {
            var tempParentPrefab = GameObject.CreatePrimitive(PrimitiveType.Quad);
            tempParentPrefab.SetActive(false);
            
            var typedPrefab = (T) prefab;
            var instantiatedComponent = Object.Instantiate(typedPrefab, tempParentPrefab.transform);
            instantiatedComponent.gameObject.SetActive(false);
            instantiatedComponent.transform.SetParent(null);
            
            Object.Destroy(tempParentPrefab);
            
            var taskSource = new UniTaskCompletionSource<T>();
            taskSource.TrySetResult(instantiatedComponent);
            return await taskSource.Task;
        }
    }
}