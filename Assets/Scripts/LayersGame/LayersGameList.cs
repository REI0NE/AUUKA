using System.Collections.Generic;
using UnityEngine;

public class LayersGameList
{
    private LayerSpriteList[] _layer = new LayerSpriteList[5];
    public LayerSpriteList[] Layer => _layer;

    public void AddSprite(int layerLevel, SpriteRenderer sprite)
    {
        _layer[layerLevel] ??= new LayerSpriteList();
        _layer[layerLevel].Sprite.Add(sprite);
    }
}
public class LayerSpriteList
{
    private List<SpriteRenderer> _sprite = null;
    public List<SpriteRenderer> Sprite => _sprite ??= new List<SpriteRenderer>();
}

