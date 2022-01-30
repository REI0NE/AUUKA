using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoCache
{
    [SerializeField] private CursorState _state = CursorState.Watch;
    [SerializeField] private LayerMask _layerDetect = 0;
    [SerializeField] private float _radiusCursor = 1f;
    [Space]
    [SerializeField] private List<CursorIcon> _iconCursor = null;

    //private BoxCollider2D _boxCollider2D = null;
    private SpriteRenderer _spriteRenderer = null;
    private Camera _camera = null;
    private EventSystem _eventSystem = null;

    private IInteractionObject _select = null;

    public CursorState State => _state;

    private void Awake()
    {
        UnityEngine.Cursor.visible = false;
        //_boxCollider2D ??= GetComponent<BoxCollider2D>();
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
        _camera ??= FindObjectOfType<Camera>();
        _eventSystem ??= FindObjectOfType<EventSystem>();
    }

    private void OnEnable() => AddUpdateTick();
    private void OnDisable() => RemoveUpdateTick();
    private void OnDestroy() => RemoveUpdateTick();

    public override void UpdateTick()
    {
       Collider[] collider2D = Physics.OverlapSphere(transform.position, _radiusCursor, _layerDetect);

        if (collider2D.Length > 0 && !_eventSystem.IsPointerOverGameObject())
        {
            if (!_singleton.Data.Settings.IsPause)
                CursorOver();
        }
        else _state = CursorState.Default;
        //if (_boxCollider2D.IsTouchingLayers(_layerDetect)) CursorOver();
        //else _state = CursorState.Default;

        if (_spriteRenderer.sprite != _iconCursor[(int)_state].Sprite)
            _spriteRenderer.sprite = _iconCursor[(int)_state].Sprite;

        Vector3 vector = _camera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _singleton.Data.Player.Poss().position.z - _camera.transform.position.z)
        );
        vector.z = _singleton.Data.Player.Poss().position.z;

        transform.position = vector;
    }


    private void CursorOver()
    {
        Collider collider2D = Physics.OverlapSphere(transform.position, _radiusCursor, _layerDetect)[0];

        if (collider2D != null)
        {
            bool stop = false;

            _singleton.Data.InteractionList.Interactions.ForEach(interaction =>
            {
                if (stop) return;

                if (interaction.Name().Equals(collider2D.name))
                {
                    _state = interaction.Type();
                    _select = interaction;
                    stop = true;
                }
            });
        }

        if (_select != null)
            if (_state == CursorState.Find)
            {
                if (Input.GetMouseButton(0))
                    _select.OnClick();
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                    _select.OnClick();
            }
        _select = null;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radiusCursor);
    }

    public Transform Poss() => transform;
}
public enum CursorState
{
    [Description("Default")]
    Default,
    [Description("Watch")]
    Watch,
    [Description("Transition")]
    Transition,
    [Description("Find")]
    Find,
    [Description("Attack")]
    Attack,
    [Description("Talk")]
    Talk
}

[System.Serializable]
public class CursorIcon
{
    [SerializeField] private string _name = null;
    [SerializeField] private Sprite _sprite = null;

    public string Name => _name;
    public Sprite Sprite => _sprite;
}