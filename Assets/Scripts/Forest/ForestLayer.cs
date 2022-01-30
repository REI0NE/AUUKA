using System.Collections.Generic;
using UnityEngine;

public class ForestLayer : MonoBehaviour
{
    [SerializeField] private float _distance = 0;

    public float Distance { get => _distance; set => _distance = value; }

    private List<SpriteRenderer> _sprites = null;

    public List<SpriteRenderer> Sprites => _sprites;

    private void Awake()
    {
        _sprites ??= new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        if (_distance > 0f)
            SetForest();
    }
    private void Start(){}

    public void SetForest(float? distance = null,bool linear = false)
    {
        distance ??= _distance;
        float xline = 0;
        for (int i = System.Convert.ToInt32(linear); i < _sprites.Count; i++)
        {
            xline += (_sprites[i- (i == 0 ? 0 : 1)].sprite.bounds.size.x * _sprites[i].transform.localScale.x) + (float)distance;
            _sprites[i].transform.position = new Vector2(xline, _sprites[i].transform.position.y);
        }
    }
}
