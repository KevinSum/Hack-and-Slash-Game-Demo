using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody;
    [SerializeField] private string[] damageSources;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ReceiveAttackSignal(string tag, float damage, float thrust, Vector3 enemy)
    {
        // Check if tag from the originated attack signal is from an entity that can deal damage to this entity.
        for (int i = 0; i < damageSources.Length; i++)
        {
            if (damageSources[i] == tag)
            {
                if (!animator.GetBool("invincible"))
                {
                    animator.SetFloat("health", animator.GetFloat("health") - damage);
                    StartCoroutine(KnockCoroutine(enemy, thrust));
                }
            }
        }
        
    }

    private IEnumerator KnockCoroutine(Vector3 enemyPosition, float thrust)
    {
        Vector2 forceDirection = transform.position - enemyPosition;
        Vector2 force = forceDirection.normalized * thrust;

        rigidbody.velocity = force;
        yield return new WaitForSeconds(.3f);

        rigidbody.velocity = new Vector2();
    }
}
