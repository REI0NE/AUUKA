using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabList : MonoBehaviour
{
    [SerializeField] private List<ItemTemp> _items = null;
    public List<ItemTemp> Items => _items;
}

[System.Serializable]
public class ItemTemp
{
    [SerializeField] private ItemClass _link = null;
    [SerializeField]
    [TextArea(5, 10)] private string _description = null;

    public string Description => _description;
    public ItemClass Link => _link;
}