using UnityEngine;

public class MyCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;

    private BoxCollider2D _boxCollider2 = null;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _boxCollider2 ??= GetComponent<BoxCollider2D>();
        float height = _camera.orthographicSize * 2f;
        _boxCollider2.size = new Vector2(height * _camera.aspect * 3f, height) * 2;
    }
}
