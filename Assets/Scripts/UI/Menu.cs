using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Menu : MonoCache
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private AudioSource _musicSource = null;
    [SerializeField] private AudioSource _soundSource = null;
    [SerializeField] private AudioSource _WalkSource = null;
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private Slider _music = null;
    [SerializeField] private Slider _sound = null;
    [SerializeField] private Toggle _postfx = null;
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Camera _cameraUI = null;

    private void Awake()
    {
        _WalkSource.volume = _soundSource.volume = _music.value = _singleton.Data.Settings.Music;
        _musicSource.volume = _sound.value = _singleton.Data.Settings.Sound;
        _music.onValueChanged.AddListener(SetMusic);
        _sound.onValueChanged.AddListener(SetSound);
        _postfx.onValueChanged.AddListener(SetPostfx);

        _postfx.isOn = _camera.GetComponent<PostProcessLayer>().enabled = _camera.GetComponent<PostProcessVolume>().enabled = _singleton.Data.Settings.Postfx;
        _cameraUI.gameObject.SetActive(_singleton.Data.Settings.Postfx);
        if (_singleton.Data.Settings.Postfx) _camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
        else _camera.cullingMask |= 1 << LayerMask.NameToLayer("UI");
        _canvas.worldCamera = _singleton.Data.Settings.Postfx ? _cameraUI : _camera;
    }

    private void OnEnable() => AddUpdateTick();
    private void OnDisable() => RemoveUpdateTick();
    private void OnDestroy() => OnDisable();

    public override void UpdateTick()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnPause();
            _animator.SetTrigger("Toggle");
        }
    }

    public void OnExitToMenu()
    {
        _singleton.Data.Settings.IsPause = !(UnityEngine.Cursor.visible = true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void OnPause() => _singleton.Data.Settings.IsPause = !_singleton.Data.Settings.IsPause;

    private void SetMusic(float value) => _musicSource.volume = _singleton.Data.Settings.Music = value;

    public void SetSound(float value) => _WalkSource.volume = _soundSource.volume = _singleton.Data.Settings.Sound = value;

    public void SetPostfx(bool value)
    {
        _camera.GetComponent<PostProcessLayer>().enabled = _camera.GetComponent<PostProcessVolume>().enabled = _singleton.Data.Settings.Postfx = value;
        _cameraUI.gameObject.SetActive(value);
        if (value) _camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
        else _camera.cullingMask |= 1 << LayerMask.NameToLayer("UI");
        _canvas.worldCamera = value ? _cameraUI : _camera;
    }
}
