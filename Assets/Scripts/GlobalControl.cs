using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make this instance of the game object that this script is attachde to persist across all scenes
public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}