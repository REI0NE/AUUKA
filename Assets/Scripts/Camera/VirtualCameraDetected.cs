using UnityEngine;

public class VirtualCameraDetected : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Camera _cameraUI = null;

    private VirtualCamera _virtualCamera = null;

    private void Awake()
    {
        _camera ??= FindObjectOfType<Camera>();
        _virtualCamera ??= FindObjectOfType<VirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("BG"))
        {
            _virtualCamera.SetOffset(new Vector3(1, 2.6f, -10));
            _virtualCamera.SetConfinerBox(collision);
            _cameraUI.orthographic = true;
            _camera.orthographic = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("BG"))
        {
            _virtualCamera.SetOffset(new Vector3(1, 5.95f, -10));
            _virtualCamera.SetConfinerBox(null);
            _cameraUI.orthographic = false;
            _camera.orthographic = false;
        }
    }
}
