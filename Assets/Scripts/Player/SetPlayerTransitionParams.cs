using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerTransitionParams : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private Vector2 initialPlayerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        transform = GetComponent<Transform>();
        transform.position = initialPlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInitialPosition(Vector2 position)
    {
        initialPlayerPosition = position;
    }
}
