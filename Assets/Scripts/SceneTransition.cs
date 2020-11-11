using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 newPlayerPosition;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            player.transform.position = newPlayerPosition;
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
