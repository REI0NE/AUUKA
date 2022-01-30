using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SideMenu : MonoBehaviour
{
    [SerializeField] private float _timeApperiancing = 0f;
    [SerializeField] private CanvasGroup _sideMenuGroup = null;

    private Sequence _sequence;
    public void AppearSideMenu()
    {
        _sequence = DOTween.Sequence();
        _sequence.AppendCallback(() =>
        {
            _sideMenuGroup.DOFade(1f, _timeApperiancing);
            _sideMenuGroup.interactable = false;
        });

        _sequence.AppendInterval(_timeApperiancing);

        _sequence.AppendCallback(() =>
        {
            _sideMenuGroup.interactable = true;
        });
    }
}
