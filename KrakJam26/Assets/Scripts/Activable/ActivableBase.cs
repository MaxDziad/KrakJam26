using UnityEngine;

public abstract class ActivableBase : MonoBehaviour
{
	public abstract void Activate();
	public virtual void Deactivate() { }
}
