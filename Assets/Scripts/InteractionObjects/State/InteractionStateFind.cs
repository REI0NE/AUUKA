using UnityEngine;

public class InteractionStateFind : MonoCache, IInteractionState
{
    [SerializeField] private ItemClass _item = null;
    [SerializeField] private int _findTime = 5;

    private Notification _notification = null;
    public ItemClass ItemClass { get => _item; set => _item = value; }

    private Singleton _singleton = Singleton.GetInstance();

    private bool _activeted = false;
    private float timer = 0f;

    private void Awake() => _notification ??= FindObjectOfType<Notification>();


    private void OnMouseExit() => timer = 0f;

    public void OnClick()
    {
        if (!_activeted)
        {
            timer += Time.deltaTime;

            if ((int)timer == _findTime)
                if (!_activeted)
                {
                    _activeted = true;
                    _notification.Call(this);
                }
        }
        else
        {
            _singleton.Data.Settings.IsPause = true;
            _notification.Call(this);
        }
    }
}
