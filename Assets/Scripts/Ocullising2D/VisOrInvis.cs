using UnityEngine;

public class VisOrInvis : MonoBehaviour
{
    private Transform _parrent = null;
    private SpriteRenderer _spriteRenderer = null;

    private void Awake()
    {
        _parrent = GetComponentInParent<Transform>();
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        transform.localScale = _spriteRenderer.bounds.size;
    }
    private void Start()
    {
        if (_spriteRenderer.enabled)
            Check();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Camera"))
            Check();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Camera"))
            Check();
    }

    private void Check() => _spriteRenderer.enabled = !_spriteRenderer.enabled;
}
