using DG.Tweening;
using UnityEngine;

public class ForestManager : MonoBehaviour
{
    [SerializeField]
    [Header("Count forest")] private int _countLevel = 5;
    [SerializeField]
    [Header("Forest to distance to z")] private float _distance = 5f;
    [SerializeField] private float _time = 1f;

    private Player _player = null;
    private Forest _forest = null;
    private Sequence _sequence = null;

    private int _playerLevel = 0;

    private Singleton _singleton = Singleton.GetInstance();

    private void Awake()
    {
        _player ??= FindObjectOfType<Player>();
        _forest ??= FindObjectOfType<Forest>();
        _player.Transition += Transition;
        DOTween.Init().SetCapacity(1250, 1250);
    }

    private void Start()
    {
        for (int i = 0; i < _countLevel; i++)
            if (i < _forest.Forests.Count)
                _forest.Forests[i].transform.position = new Vector3(_forest.Forests[i].transform.position.x, _forest.Forests[i].transform.position.y, i * _distance);
    }

    private void Transition(Vector3 direction)
    {
        if (!_singleton.Data.Settings.IsPause)
        {
            _sequence = DOTween.Sequence();

            if (direction == Vector3.up)
            {
                if (_playerLevel < _countLevel - 1)
                {
                    _playerLevel++;
                    _sequence.Insert(0, _player.Poss().DOMoveZ((_playerLevel * _distance), _time));
                    _singleton.Data.LayersGameList.Layer[_playerLevel - 1].Sprite.ForEach(tree => _sequence.Insert(0, tree.DOFade(0, _time)));
                    _sequence.Play();
                    return;
                }
            }
            else if (direction == Vector3.down)
            {
                if (_playerLevel > 0)
                {
                    _playerLevel--;
                    _sequence.Insert(0, _player.Poss().DOMoveZ((_playerLevel * _distance), _time));
                    _singleton.Data.LayersGameList.Layer[_playerLevel].Sprite.ForEach(tree => _sequence.Insert(0, tree.DOFade(1, _time)));
                    _sequence.Play();
                    return;
                }
            }
        }
    }

    public void Stopped() => _sequence.Kill();
}
