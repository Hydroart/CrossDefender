using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    // Bounds for the camera
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        Cursor.visible = false;
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        // Move the camera towards the target
        Vector3 newPosition = Target.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);

        // Clamp the camera position to stay within the specified bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );

        // Shake the camera if the shakeDuration is greater than 0
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void ShakeCamera()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 0.2f;
    }
}
