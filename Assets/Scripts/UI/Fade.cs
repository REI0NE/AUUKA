using DG.Tweening;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private Canvas _canvas = null;
    private CanvasGroup _canvasGroup = null;

    private Sequence _sequence = null;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _canvas ??= GetComponent<Canvas>();
        _canvasGroup ??= GetComponent<CanvasGroup>();
        _canvas.enabled = false;
    }

    public void Execute(Vector3 poss, float time = 1f)
    {
        _canvas.enabled = true;
        _sequence = DOTween.Sequence();
        _sequence.Append(_canvasGroup.DOFade(1,time));
        _sequence.Append(_singleton.Data.Player.Poss().DOMove(poss,time));
        _sequence.Append(_canvasGroup.DOFade(0, time)).OnComplete(() => _singleton.Data.Settings.IsPause = _canvas.enabled = false);
        _sequence.Play();
    }
}
