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

    public void ReceiveAttackSignal(string tag, float damage, float thrust, Vector3 enemyPos)
    {
        // Check if tag from the originated attack signal is from an entity that can deal damage to this entity.
        for (int i = 0; i < damageSources.Length; i++)
        {
            if (damageSources[i] == tag)
            {
                if (!animator.GetBool("invincible"))
                {
                    animator.SetFloat("health", animator.GetFloat("health") - damage);

                    StartCoroutine(KnockbackCoroutine(enemyPos, thrust));
                }
            }
        }
        
    }

    private IEnumerator KnockbackCoroutine(Vector3 enemyPos, float thrust)
    {
        animator.SetTrigger("staggered");
        animator.SetBool("canMove", false);

        // Apply force knockback
        Vector2 forceDirection = transform.position - enemyPos;
        Vector2 force = forceDirection.normalized * thrust;
        rigidbody.velocity = force;

        yield return new WaitForSeconds(0.001f); // wait one frame for animator to switch to staggered animation. Then we can get the length of the animation;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.001f);

        rigidbody.velocity = new Vector2();

        animator.SetBool("canMove", true);
    }

}
