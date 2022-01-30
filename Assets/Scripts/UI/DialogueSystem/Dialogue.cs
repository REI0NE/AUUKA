using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] private string _name = null;
    [SerializeField] private Replica[] _replicas = null;
    public string Name => _name;
    public Replica[] Replicas => _replicas;
}

[System.Serializable]
public class Replica
{
    [SerializeField] private string _name = null;
    [TextArea(5, 10)]
    [SerializeField] private string _content = null;

    public string Name => _name;
    public string Content => _content;
}