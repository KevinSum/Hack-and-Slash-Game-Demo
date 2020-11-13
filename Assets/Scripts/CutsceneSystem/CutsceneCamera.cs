using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneCamera : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCamPriority(int camPriority)
    {
        cinemachineVirtualCamera.m_Priority = camPriority;
    }

    void setInCutscene(bool inCutsceneBool)
    {
        playerAnimator.SetBool("inCutscene", inCutsceneBool);
        playerAnimator.SetBool("canMove", !inCutsceneBool);
    } 
}
