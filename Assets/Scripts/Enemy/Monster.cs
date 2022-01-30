using UnityEngine;
using DG.Tweening;

public class Monster : Enemy, IEnemy
{

    private void Awake()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
        _boxCollider2D ??= GetComponent<BoxCollider2D>();
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        AddUpdateTick();
        _singleton.Data.EnemyList.Add(this);
    }

    private void OnDisable()
    {
        RemoveUpdateTick();
        _singleton.Data.EnemyList.Remove(this);
    }

    private void OnDestroy() => OnDisable();

    public override void UpdateTick()
    {
        Movement();
        if (_stats.GetStat("HP") <= 0) Die(.3f);
    }

    public string Name() => name;

    public Stats GetStats() => _stats;
}
