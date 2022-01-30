using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    private List<ItemPrefabList> _storageItem = null;

    private void Awake() => _storageItem ??= new List<ItemPrefabList>(GetComponentsInChildren<ItemPrefabList>());

    public ItemPrefabList StorageItemLink(Language language) => _storageItem[(int)language];
}
