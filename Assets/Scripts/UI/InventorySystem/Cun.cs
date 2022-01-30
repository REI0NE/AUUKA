
public class Cun : ItemClass
{
    private DialogueStorage _dialogueStorage = null;
    private InventoryManager _inventoryManager = null;
    private Singleton _singleton = Singleton.GetInstance();

    public override void DoExecute(ref InventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
        _dialogueStorage = FindObjectOfType<DialogueStorage>();
    }

    public override void Execute()
    {
        _dialogueStorage.StorageDialogue(_singleton.Data.Settings.Language).StartDialogue("konserva");
        _inventoryManager.RemoveItem(this);
    }
}
