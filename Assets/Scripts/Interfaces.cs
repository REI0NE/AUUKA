using UnityEngine;
public interface IUnit
{
    public string Name();
    public Stats GetStats();
}

public interface IPlayer : IUnit 
{
    public Transform Poss();
    public PlayerState GetState();
    public void SwitchMod();
}
public interface IEnemy : IUnit { }
public interface IInteractionObject
{
    public string Name();
    public CursorState Type();
    public void SwitchType(CursorState state);
    public void OnClick();
}

public interface IInteractionState
{
    public void OnClick();
}