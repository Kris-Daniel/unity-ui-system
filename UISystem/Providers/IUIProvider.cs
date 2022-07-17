using Cysharp.Threading.Tasks;
using UISystem.UITypes;

namespace UISystem.Providers
{
    public interface IUIProvider
    {
        UniTask<T> InstantiatePrefabInactiveAsync<T>(object prefab) where T : UIEntity;
    }
}