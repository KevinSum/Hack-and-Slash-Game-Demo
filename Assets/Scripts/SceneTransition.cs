using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 newPlayerPosition;
    [SerializeField] private SetPlayerTransitionParams playerTransitionParams;
    private GameObject player;

    private void Start()
    {
        playerTransitionParams = GameObject.Find("Player").GetComponent<SetPlayerTransitionParams>();
        player = GameObject.Find("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerTransitionParams.setInitialPosition(newPlayerPosition);
            player.transform.position = newPlayerPosition;
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
