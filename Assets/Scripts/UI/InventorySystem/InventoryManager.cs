using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<ItemClass> _items = null;
    [SerializeField] private List<GameObject> _slots = null;
    [SerializeField] private GameObject _slotsHolder = null;
    private void Awake()
    {
        for (int i = 0; i < _slotsHolder.transform.childCount; i++)
            _slots[i] = _slotsHolder.transform.GetChild(i).gameObject;

        RefreshUI();
    }

    public bool AddItem(ItemClass item)
    {
        if (_items.Count < _slots.Count)
        {
            _items.Add(item);
            RefreshUI();
            return true;
        }
        return false;
    }
    public void RemoveItem(ItemClass item)
    {
        if (_items.Count > 0)
        {
            _items.Remove(item);
            RefreshUI();
        }
    }
    private void RefreshUI()
    {
        InventoryManager inventoryManager = this;

        for (int i = 0; i < _slots.Count; i++)
        {
            try
            {
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].ItemIcon;
                _items[i].DoExecute(ref inventoryManager);
                int index = i;
                _slots[index].GetComponent<Button>().onClick.RemoveAllListeners();
                _slots[index].GetComponent<Button>().onClick.AddListener(() => _items[index].Execute());
            }
            catch
            {
                _slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                _slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                _slots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
}
