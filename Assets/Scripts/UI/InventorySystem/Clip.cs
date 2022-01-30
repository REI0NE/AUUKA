using UnityEngine;
public class Clip : ItemClass
{
    [SerializeField] private int _bulletCount = 7;

    private InventoryManager _inventoryManager = null;

    private Singleton _singleton = Singleton.GetInstance();

    public override void DoExecute(ref InventoryManager inventoryManager)
    {
        if(_inventoryManager == null)
            _inventoryManager = inventoryManager;
    }

    public override void Execute()
    {
        if (_singleton.Data.Player != null)
            if (_singleton.Data.Player.GetState() == PlayerState.Attack)
                if (_singleton.Data.Player.GetStats().GetStat("Bullet") == 0)
                {
                    _singleton.Data.Player.GetStats().GetVariable("Bullet").Variable = _bulletCount;
                    _inventoryManager.RemoveItem(this);
                }
    }
}
