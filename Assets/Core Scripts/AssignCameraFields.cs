using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Set cinecamera component in the same gameObject to follow gameObject Player upon loading.
public class AssignCameraFields : MonoBehaviour
{
    Transform playerTransform;
    CinemachineBrain cinemachineBrain;
    CinemachineVirtualCamera cinemachineCamera;
    CinemachineConfiner cinemachineConfiner;
    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        cinemachineBrain = GameObject.Find("Main Camera").transform.GetComponent<CinemachineBrain>();
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineConfiner = GetComponent<CinemachineConfiner>();

        // Check if camera is following 'player' GameObject
        if (playerTransform != null)
            cinemachineCamera.Follow = playerTransform;
        else
            Debug.LogWarning("Can't find 'Player' game object for cinemachine camera to follow. " +
                "Either set Follow field in CinemachineCamera manually, or create a game object called player (preferably using the prefab) for camera to follow");

        // Note that the Bound Shape 2D field can't be set through script. Must be set through inspector.
        if (cinemachineConfiner.m_BoundingShape2D == null)
        {
            Debug.LogWarning("No camera bounding set! If needed, create a 'PolygonCollider2D' component, create the boundary you want, " +
                "set the 'IsTrigger' field to true, and assign the component to the 'Bounding Shape 2D' field in the 'Cinemachine Confiner'", this.transform);
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
