using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamage : MonoBehaviour
{
    // Start is called before the first frame update
    // Knockback and damage
    [SerializeField] private float attackThrust;
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player")) 
            || (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Enemy")))
        {
            collision.GetComponent<ReceiveDamage>().ReceiveAttackSignal(gameObject.tag, damage, attackThrust, transform.position);

        }
    }

}
