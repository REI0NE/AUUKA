using UnityEngine;

public class InteractionObject : MonoBehaviour, IInteractionObject
{
    [SerializeField] private CursorState _type = CursorState.Talk;

    private IInteractionState _currentState = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake() => _currentState ??= GetComponent<IInteractionState>();

    private void Start() => name = GetComponentsInParent<Transform>()[1].name + "_" + _type + "_" + name + Random.Range(-2599999, 2599999).ToString();

    private void OnEnable() => _singleton.Data.InteractionList.Interactions.Add(this);

    private void OnDisable() => _singleton.Data.InteractionList.Interactions.Remove(this);

    private void OnDestroy() => OnDisable();

    public string Name() => name;
    public CursorState Type() => _type;

    public void SwitchType(CursorState state) => _type = state;

    public void OnClick()
    {
        if (_currentState != null) _currentState.OnClick();
        else Debug.Log("Не добавлен скрипт который выполнит резуальтат!");
    }

}