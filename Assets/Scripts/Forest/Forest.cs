using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    [SerializeField] private bool _oneAllTree = false;
    [SerializeField] private float _totalDistance = 0f;
    [SerializeField] private bool _activeRandom = false;
    [SerializeField] private List<Vector2> _rand = null;

    private List<ForestLayer> _forestLayers = null;

    public List<ForestLayer> Forests => _forestLayers;

    private void Awake() => _forestLayers ??= new List<ForestLayer>(GetComponentsInChildren<ForestLayer>());

    private void Start()
    {
        if (_oneAllTree)
            TotalDistance();

        if (_activeRandom)
            Random();
    }
    private void TotalDistance(float? totalDistance = null)
    {
        totalDistance ??= _totalDistance;
        _forestLayers.ForEach(forestLayer => forestLayer.SetForest(totalDistance));
    }

    private void Random()
    {
        for (int i = 0; i < _forestLayers.Count; i++)
            if (i < _rand.Count)
                _forestLayers[i].SetForest(UnityEngine.Random.Range(_rand[i].x, _rand[i].y), i % 2 == 0);
    }
}
