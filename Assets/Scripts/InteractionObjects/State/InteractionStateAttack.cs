using UnityEngine;

public class InteractionStateAttack : MonoBehaviour, IInteractionState
{
    private IEnemy _enemy = null;
    private Audio _audio = null;
    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _enemy ??= GetComponent<IEnemy>();
        _audio ??= FindObjectOfType<Audio>();
    }

    public void OnClick()
    {
        if (_singleton.Data.Player.GetState() == PlayerState.Attack)
            if (_singleton.Data.Player.GetStats().GetStat("Bullet") > 0)
            {
                _audio.OneShot("Fire");
                _enemy.GetStats().GetVariable("HP").Variable -= _singleton.Data.Player.GetStats().GetStat("Damage");
            }
    }
}