using UnityEngine;

public class ScreamDamageReceiver : MonoBehaviour
{
    [SerializeField]
    private ActivableBase _activateOnDamage;

    [SerializeField]
    private ActivableBase _deactivateOnDamage;

    public void ReceiveDamage()
    {
        _activateOnDamage?.Activate();
        _deactivateOnDamage?.Deactivate();
	}
}
