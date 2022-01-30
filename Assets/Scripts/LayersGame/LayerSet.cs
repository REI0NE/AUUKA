using UnityEngine;

public class LayerSet : MonoBehaviour
{
    [SerializeField] private int _layerLevel = 0;

    private Singleton _singleton = Singleton.GetInstance();

    private SpriteRenderer _sprite = null;

    private void Awake() => _sprite ??= GetComponent<SpriteRenderer>();

    private void OnEnable() => _singleton.Data.LayersGameList.AddSprite(_layerLevel, _sprite);

    private void OnDisable() => _singleton.Data.LayersGameList.Layer[_layerLevel].Sprite.Remove(GetComponent<SpriteRenderer>());

}
