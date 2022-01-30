using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _windowTransform = null;
    [SerializeField] private CanvasGroup _windowGroup = null;
    [SerializeField] private float _timeOffsetingWindow = 0f;
    [SerializeField] private Image _vignette = null;
    [Space]
    [SerializeField] private AudioSource _audio = null;
    [SerializeField] private Slider _music = null;
    [SerializeField] private Slider _sound = null;
    [SerializeField] private Toggle _postfx = null;

    private float _offsetWindow;
    private Sequence _sequence;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        if (_audio == null)
            _audio = FindObjectOfType<AudioSource>();
        _music.value = _singleton.Data.Settings.Music;
        _sound.value = _singleton.Data.Settings.Sound;
        _postfx.isOn = _singleton.Data.Settings.Postfx;
        _sound.onValueChanged.AddListener(SetSound);
        _music.onValueChanged.AddListener(SetMusic);
        _postfx.onValueChanged.AddListener(SetPostfx);
        _audio.volume = _music.value;
        _audio.loop = true;
        UnityEngine.Cursor.visible = true;
    }

    private void Start() => _offsetWindow = _windowTransform.position.x;
    public void LoadGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void ExitGame() =>  Application.Quit();
    public void MoveToCenterOfWindow()
    {
        _sequence = DOTween.Sequence();
        _sequence.AppendCallback(()=>
        {
            _windowGroup.DOFade(1f, _timeOffsetingWindow);
            _vignette.DOFade(1f, _timeOffsetingWindow);
            _vignette.raycastTarget = true;
            _windowTransform.DOMove(new Vector3(0f, 0f, _windowTransform.position.z), _timeOffsetingWindow, false);
        });

        _sequence.AppendInterval(_timeOffsetingWindow);
        _sequence.AppendCallback(() =>
        {
            _sequence.Kill();
        });
    }

    public void MoveBeyondTheBorder()
    {
        _sequence = DOTween.Sequence();
        _sequence.AppendCallback(() =>
        {
            _windowGroup.DOFade(0f, _timeOffsetingWindow);
            _vignette.DOFade(0f, _timeOffsetingWindow);
            _vignette.raycastTarget = false;
            _windowTransform.DOMove(new Vector3(_offsetWindow, 0f, _windowTransform.position.z), _timeOffsetingWindow, false);
        });

        _sequence.AppendInterval(_timeOffsetingWindow);
        _sequence.AppendCallback(() =>
        {
            _sequence.Kill();
        });
    }

    public void SetMusic(float value)
    {
        _singleton.Data.Settings.Music = value;
        _audio.volume = value;
    }

    public void SetSound(float value) => _singleton.Data.Settings.Sound = value;

    public void SetPostfx(bool value) => _singleton.Data.Settings.Postfx = value;
}
