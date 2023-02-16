using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minZoom = 1f;
    public float maxZoom = 100f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        var camera = Camera.main;
        var zoom = camera.fieldOfView - scroll * zoomSpeed;
        camera.fieldOfView = Mathf.Clamp(zoom, minZoom, maxZoom);
    }
}
