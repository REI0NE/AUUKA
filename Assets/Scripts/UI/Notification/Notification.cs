using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Content = null;
    [SerializeField] private Image _slots = null;
    [SerializeField] private float _time = 2f;

    private Canvas _canvas = null;
    private CanvasGroup _canvasGroup = null;
    private List<Button> _buttons = null;

    private StorageItem _storageItem = null;
    private InventoryManager _inventory = null;
    private InteractionStateFind _item = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _storageItem ??= FindObjectOfType<StorageItem>();
        _inventory ??= FindObjectOfType<InventoryManager>();
        _canvas ??= GetComponent<Canvas>();
        _canvasGroup ??= GetComponent<CanvasGroup>();
        _buttons ??= new List<Button>(GetComponentsInChildren<Button>());
        _buttons.ForEach(but => but.onClick.AddListener(() => ButtonClick(but)));
        _canvas.enabled = false;
    }

    private void ButtonClick(Button button)
    {
        switch (button.name)
        {
            case "TakeButton":
                if (_inventory.AddItem(_item.ItemClass))
                    _item.ItemClass = null;
                break;
        }

        _singleton.Data.InteractionList.Interactions.ForEach(io => io.SwitchType(io.Name().Equals(_item.name) ? CursorState.Watch : io.Type()));
        _singleton.Data.Settings.IsPause = false;
        CheckVisible(false);
    }

    private string GetDescription()
    {
        string description = null;
        _storageItem.StorageItemLink(_singleton.Data.Settings.Language).Items.ForEach(item => description = _item.ItemClass == item.Link ? item.Description : description);
        return description;
    }

    private void CheckVisible(bool visible) => _canvasGroup.DOFade(System.Convert.ToInt32(_canvas.enabled = _slots.enabled = visible), _time);

    public void Call(InteractionStateFind item = null)
    {
        _item = item;
        _slots.color = _item.ItemClass != null ? new Color(1,1,1,1) : new Color(1,1,1,0);
        _Content.text = GetDescription();

        if (item.ItemClass != null)
            _slots.sprite = item.ItemClass.ItemIcon;

        CheckVisible(true);
    }

}
