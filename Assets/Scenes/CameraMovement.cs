using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraSmoothing;
    public Vector2 maxCameraPosition;
    public Vector2 minCameraPosition;
    void Start()
    {
        
    }

    // Late update always comes after all other updates. 
    // Camera movement should come after final player position is set.
    void LateUpdate()
    {
        if (transform.position != playerTransform.position)
        {
            // Find camera target. Z position needs to stay consistant (-10), and is not the same as player position X.
            Vector3 cameraTarget = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
            // Bound camera
            cameraTarget.x = Mathf.Clamp(cameraTarget.x, minCameraPosition.x, maxCameraPosition.x);
            cameraTarget.y = Mathf.Clamp(cameraTarget.y, minCameraPosition.y, maxCameraPosition.y);
            // Move camera towards target (which should be set to player position)
            transform.position = Vector3.Lerp(transform.position, cameraTarget, cameraSmoothing);
        }
    }
}
