using UnityEngine;
using DG.Tweening;
using TMPro;

public class End : MonoBehaviour
{
    [SerializeField] private float _time = 1f;
    private Canvas _canvas = null;
    private CanvasGroup _canvasGroup = null;
    private TextMeshProUGUI _text = null;

    private void Awake()
    {
        _canvas ??= GetComponent<Canvas>();
        _canvasGroup ??= GetComponent<CanvasGroup>();
        _text ??= GetComponentInChildren<TextMeshProUGUI>();
        _text.text = null;
        _canvas.enabled = false;
    }

    public void TheEnd(string text)
    {
        _canvas.enabled = true;
        _canvasGroup.DOFade(1,_time);
        _text.text = text;
    }
}
