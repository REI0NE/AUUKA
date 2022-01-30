using UnityEngine;

public class InteractionStateTransition : MonoBehaviour, IInteractionState
{
    [SerializeField] private string _nameMusicClip = null;
    [SerializeField] private string _nameSoundClip = null;
    [SerializeField] private float _time = 1;
    [SerializeField] private Transform _newPoss = null;

    private Fade _fade = null;
    private ForestManager _forestManager = null;
    private Audio _audio = null;
    private Singleton _singleton = Singleton.GetInstance();


    private void Awake()
    {
        _forestManager ??= FindObjectOfType<ForestManager>();
        _fade ??= FindObjectOfType<Fade>();
        _audio ??= FindObjectOfType<Audio>();
    }
    private void Start() { }

    public void OnClick()
    {
        if (_newPoss != null)
        {
            _audio.SwitchMusic(_nameMusicClip);
            _audio.OneShot(_nameSoundClip);
            _singleton.Data.Settings.IsPause = true;
            _forestManager.Stopped();
            _fade.Execute(_newPoss.position,_time);
        }
    }
}
