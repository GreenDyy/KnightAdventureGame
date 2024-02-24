using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isRight = true;
    private DamageReceiver damageReceiver;
    private DamageSender damageSender;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageReceiver = GetComponent<DamageReceiver>();
        damageSender = GetComponent<DamageSender>();
    }

    void Update()
    {
        if(!damageReceiver.IsDead() && !damageSender.isAttacking)
            Scount();
    }

    public void Scount()
    {
        if (!gameObject.GetComponent<FollowPlayer>().isChasing)
        {
            if (transform.localScale.x == -1)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            animator.SetBool("IsChasing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        if (collision.gameObject.layer == groundLayer)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int wallLayer = LayerMask.NameToLayer("Wall");
        if (collision.gameObject.layer == wallLayer)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }
}
