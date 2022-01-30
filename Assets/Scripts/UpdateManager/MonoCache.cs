using UnityEngine;

public class MonoCache : MonoBehaviour
{
    protected Singleton _singleton = Singleton.GetInstance();

    protected void AddLateTick() => _singleton.Data.MonoCacheList.GetLateTicks.Add(this);
    protected void AddUpdateTick() => _singleton.Data.MonoCacheList.GetUpdateTicks.Add(this);
    protected void AddFixedTick() => _singleton.Data.MonoCacheList.GetFixedTicks.Add(this);

    protected void RemoveLateTick() => _singleton.Data.MonoCacheList.GetLateTicks.Remove(this);
    protected void RemoveUpdateTick() => _singleton.Data.MonoCacheList.GetUpdateTicks.Remove(this);
    protected void RemoveFixedTick() => _singleton.Data.MonoCacheList.GetFixedTicks.Remove(this);

    public virtual void LateTick() { }
    public virtual void UpdateTick() { }
    public virtual void FixedTick() { }
}
