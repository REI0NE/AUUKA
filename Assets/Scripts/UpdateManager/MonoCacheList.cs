using System.Collections.Generic;

public class MonoCacheList
{
    private List<MonoCache> _lateTick = new List<MonoCache>(101);
    private List<MonoCache> _updateTick = new List<MonoCache>(101);
    private List<MonoCache> _fixedTick = new List<MonoCache>(101);

    public List<MonoCache> GetLateTicks => _lateTick;
    public List<MonoCache> GetUpdateTicks => _updateTick;
    public List<MonoCache> GetFixedTicks => _fixedTick;
}
