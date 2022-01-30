using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private List<FloatVariable> _statList = null;
    public List<FloatVariable> GetStats => _statList;
    public float GetStat(string name)
    {
        float result = 0f;
        bool stop = false;

        _statList.ForEach(var =>
        {
            if (stop) return;

            if (var.Name.Equals(name))
            {
                result = var.Variable;
                stop = true;
            }
        });

        return result;
    }
    public FloatVariable GetVariable(string name)
    {
        FloatVariable floatVariable = null;
        bool stop = false;

        _statList.ForEach(var =>
        {
            if (stop) return;

            if (var.Name.Equals(name))
            {
                floatVariable = var;
                stop = true;
            }
        });

        return floatVariable;
    }
}

[System.Serializable]
public class FloatVariable
{
    [SerializeField] private string _name = null;
    [SerializeField] private float _variable = 0f;

    public string Name => _name;
    public float Variable { get => _variable; set => _variable = value; }
}
