using UnityEngine;

public class MaskPickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] 
    private MaskType maskType;

    public void Interact()
    {
        MaskSystemManager.Instance.UnlockMask(maskType);

        Destroy(gameObject);
    }
}
