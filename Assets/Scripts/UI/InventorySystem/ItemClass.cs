using UnityEngine;

public class ItemClass : MonoBehaviour
{
    [SerializeField] protected Sprite _itemIcon = null;

    public Sprite ItemIcon => _itemIcon;

    public virtual void DoExecute(ref InventoryManager inventoryManager) {}
    public virtual void Execute() {}
}
