using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.3f;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(deltaMovement.x * parallaxEffect, 0f, 0f);

        lastCameraPosition = cameraTransform.position;
    }
}
