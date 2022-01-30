using UnityEngine;

public class Note : ItemClass
{
    [SerializeField] private int number = 0;

    private InventoryManager _inventoryManager = null;
    private Diary _diary = null;

    public override void DoExecute(ref InventoryManager inventoryManager)
    {
        if (_inventoryManager == null)
            _inventoryManager = inventoryManager;

        if (_diary == null)
            _diary = FindObjectOfType<Diary>();
    }

    public override void Execute()
    {
        _diary.NewNote(number);
        _inventoryManager.RemoveItem(this);
        _diary.Open(number);
    }
}
