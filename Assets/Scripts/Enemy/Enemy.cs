using DG.Tweening;
using UnityEngine;

public class Enemy : MonoCache
{
    [SerializeField] protected EnemyState _state = EnemyState.Idle;
    [SerializeField] protected Stats _stats = null;

    protected Rigidbody _rigidbody = null;
    protected BoxCollider2D _boxCollider2D = null;
    protected SpriteRenderer _spriteRenderer = null;

    protected Vector2 _direction = Vector2.right;
    public EnemyState State { get => _state;  set => _state = value; }

    protected virtual void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected virtual void IsFlip()
    {
        if (_direction == Vector2.right && transform.localScale.x < 0f)
            Flip();
        if (_direction == Vector2.left && transform.localScale.x > 0f)
            Flip();
    }

    protected virtual void Movement(string param = null)
    {
        Vector2 velocity = Vector2.zero;
        velocity.Set(_direction.x * _stats.GetStat(param ?? "Walk"), _rigidbody.velocity.y);
        _rigidbody.velocity = velocity;
    }

    protected virtual void Die(float? duration = null)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_spriteRenderer.DOFade(0f, duration ?? _stats.GetStat("Die")));
        sequence.AppendInterval(duration ?? _stats.GetStat("Die"));
        sequence.AppendCallback(() => Destroy(gameObject));
    }
}
public enum EnemyState
{
    Idle,
    Follow,
    Attack,
    Talk,
    Die
}