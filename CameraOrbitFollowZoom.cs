using UnityEngine;
[AddComponentMenu("Camera-Control/Camera Orbit Follow Zoom")]
public class CameraOrbitFollowZoom : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 5.0f;
    [SerializeField] float xSpeed = 10.0f;
    [SerializeField] float ySpeed = 10.0f;

    [SerializeField] float yMinLimit = -90f;
    [SerializeField] float yMaxLimit = 90f;

    [SerializeField] float distanceMin = 5f;
    [SerializeField] float distanceMax = 500f;

    [SerializeField] float smoothTime = 0.2f;

    float x = 0.0f;
    float y = 0.0f;
    float xSmooth = 0.0f;
    float ySmooth = 0.0f;
    float xVelocity = 0.0f;
    float yVelocity = 0.0f; 
    float distanceSmooth;
    float distanceVelocity=0.0f;
    void LateUpdate()
    {
        if (target)
        {
            GetInputs();
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        xSmooth = Mathf.SmoothDamp(xSmooth, x, ref xVelocity, smoothTime);
        ySmooth = Mathf.SmoothDamp(ySmooth, y, ref yVelocity, smoothTime);
        distanceSmooth = Mathf.SmoothDamp(distanceSmooth, distance,ref distanceVelocity, smoothTime);
        transform.localRotation = Quaternion.Euler(ySmooth, xSmooth, 0);
        transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -distanceSmooth) + target.position;
    }

    private void GetInputs()
    {
        if (Input.GetMouseButton(1))
        { 
            x += Input.GetAxis("Mouse X") * xSpeed;
            y -= Input.GetAxis("Mouse Y") * ySpeed;

            y = Mathf.Clamp(y, yMinLimit, yMaxLimit); 
        }

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * distance, distanceMin, distanceMax); 
    }
}
