
public class Gun : ItemClass
{
    private Singleton _singleton = Singleton.GetInstance();

    public override void Execute()
    {
        if (_singleton.Data.Player != null)
            _singleton.Data.Player.SwitchMod();
    }
}
