using UnityEngine;

public class Weapon : MonoCache
{
    [SerializeField] private Transform _firePoint = null;
    [SerializeField] private GameObject _bulletPrefab = null;

    private Player _player = null;
    private Cursor _cursor = null;
    private void Awake()
    {
        _player ??= GetComponent<Player>();
        _cursor ??= FindObjectOfType<Cursor>();
    }
    private void OnEnable() => AddUpdateTick();
    private void OnDisable() => RemoveUpdateTick();
    private void OnDestroy() => OnDisable();

    public override void UpdateTick()
    {
        if (_player.GetState() == PlayerState.Attack && _cursor.State == CursorState.Attack)
        {
            if (Input.GetMouseButtonUp(0))
                if (_player.GetStats().GetStat("Bullet") > 0)
                    Shoot();

        }
    }

    private void Shoot()
    {
        _player.GetStats().GetVariable("Bullet").Variable--;
        Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
