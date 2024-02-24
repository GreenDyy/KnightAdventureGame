using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFollowPlayer : FollowPlayer
{
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        damageReceiver = GetComponent<TPDamageReceiver>();
        damageSender = GetComponent<TPDamageSender>();
    }

    void FixedUpdate()
    {
        float distanceEnemyToPlayer = Vector3.Distance(player.position, transform.position);
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, player.position - transform.position, distanceEnemyToPlayer);
        Debug.DrawRay(transform.position, player.position - transform.position, Color.red);
        if (hitObstacle.collider != null && hitObstacle.collider.gameObject.name != "Player")
        {
            isChasing = false;
        }
        if (distanceEnemyToPlayer <= 8f && !damageReceiver.IsDead())
        {
            Flip();
            isChasing |= true;
        }
    }
}
