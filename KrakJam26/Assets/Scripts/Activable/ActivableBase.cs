using UnityEngine;

public abstract class ActivableBase : MonoBehaviour
{
	public abstract void Activate();
	protected virtual void Deactivate() { }
}
