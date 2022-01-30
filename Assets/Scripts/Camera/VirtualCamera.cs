using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineConfiner _confiner = null;
    private CinemachineVirtualCamera _camera = null;
    private CinemachineTransposer _cinemachineCamera = null;

    private void Awake()
    {
        _confiner ??= GetComponent<CinemachineConfiner>();
        _camera ??= GetComponent<CinemachineVirtualCamera>();
        _cinemachineCamera ??= _camera.GetCinemachineComponent<CinemachineTransposer>();
    }
    public void SetConfinerBox(Collider2D box) => _confiner.m_BoundingShape2D = box;
    public void SetOffset(Vector3 follow) => _cinemachineCamera.m_FollowOffset = follow;
    public void SetSize(float size) => _camera.m_Lens.OrthographicSize = size;
}
