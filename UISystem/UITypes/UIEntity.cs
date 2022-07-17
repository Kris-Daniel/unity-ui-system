using UnityEngine;

namespace UISystem.UITypes
{
	public abstract class UIEntity : MonoBehaviour
	{
		protected object CustomData;
		
		public virtual void Init(object customData = null)
		{
			CustomData = customData;
		}
	}
}