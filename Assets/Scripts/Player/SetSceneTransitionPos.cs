using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

// Set initial position during scene transition
public class SetSceneTransitionPos : MonoBehaviour
{
    [SerializeField] private Vector2 initialPos;
    private GameObject cinemachineCamera;
    private GameObject camera;

    private void Awake()
    {
        cinemachineCamera = GameObject.Find("Cinemachine Camera");
        camera = GameObject.Find("Main Camera");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if object has been destroyed. For some reason, this class will persist, even if destroyed.
        if (this)
        {
            //cinemachineCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            this.transform.position = initialPos;


            //cinemachineCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        }
            

    }
    
    public void SetInitialPos(Vector2 inputPos)
    {
        initialPos = inputPos;
    }
}
