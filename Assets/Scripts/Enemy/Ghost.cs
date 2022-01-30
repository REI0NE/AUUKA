using DG.Tweening;
using UnityEngine;

public class Ghost : Enemy, IEnemy
{
    [SerializeField] private Transform _nps = null;

    public Stats GetStats() => _stats;

    public string Name() => name;

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
        IsFlip();
        if (_stats.GetStat("HP") <= 0) Die(.3f);
    }


    protected override void IsFlip()
    {
        if (_singleton.Data.Player.Poss().position.x > transform.position.x && transform.localScale.x > 0f)
            Flip();

        if (_singleton.Data.Player.Poss().position.x < transform.position.x && transform.localScale.x < 0f)
            Flip();
    }
    protected override void Die(float? duration = null)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_spriteRenderer.DOFade(0f, duration ?? _stats.GetStat("Die")));
        sequence.AppendInterval(duration ?? _stats.GetStat("Die"));
        sequence.AppendCallback(() =>
        {
            _nps.position = transform.position;
            _nps.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
