using UnityEngine;
using TMPro;
using DG.Tweening;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Canvas _startScreen = null;
    [SerializeField] private TextMeshProUGUI _startText = null;
    [SerializeField] private float _scalePulsation = 0f;
    [SerializeField] private float _durationPulsation = 0f;

    private CanvasGroup _startScreenGroup;
    private void Awake() => _startScreenGroup = _startScreen.GetComponent<CanvasGroup>();
    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        float endScale = _startText.fontSize + _scalePulsation;

        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Append(DOTween.To(x => _startText.fontSize = x, _startText.fontSize, endScale, _durationPulsation));
    }

    public void ScreenFading(float scaleFading)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_startScreenGroup.DOFade(0f, scaleFading));
        sequence.AppendCallback(() =>
        {
            _startScreen.gameObject.SetActive(false);
        });
    }
}
