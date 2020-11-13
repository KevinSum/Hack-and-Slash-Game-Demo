using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 newPlayerPosition;
    private GameObject player;
    private SetSceneTransitionPos playerInitialPos;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            player = GameObject.Find("Player");
            playerInitialPos = player.GetComponent<SetSceneTransitionPos>();
            playerInitialPos.SetInitialPos(newPlayerPosition);
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
