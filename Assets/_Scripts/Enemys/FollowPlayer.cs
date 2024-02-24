using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speedChase = 2f;
    public float defaultDistanceTwoObject = 1.5f; // tầm này enemy k dí nữa
    public bool isChasing = true;
    public Animator myAnimator;

    public DamageReceiver damageReceiver;
    public DamageSender damageSender;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        damageReceiver = GetComponent<DamageReceiver>();
        damageSender = GetComponent<DamageSender>();
    }

    void FixedUpdate()
    {
        float distanceEnemyToPlayer = Vector3.Distance(player.position, transform.position);
        // Kiểm tra xem có vật cản giữa người chơi và kẻ địch không
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, player.position - transform.position, distanceEnemyToPlayer);
        Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

        if (hitObstacle.collider != null && hitObstacle.collider.gameObject.name != "Player")
        {
            // Có vật cản, không cho kẻ địch theo đuổi
            myAnimator.SetBool("IsChasing", false);
            isChasing = false;
        }
        else if (distanceEnemyToPlayer <= 8f && !damageSender.isAttacking && !damageReceiver.IsDead())
        {
            myAnimator.SetBool("IsChasing", true);
            isChasing = true;
            Flip();
            Follow();
        }
        else if (damageSender.isAttacking)
        {
            myAnimator.SetBool("IsChasing", false);
            isChasing = false;
        }
        else
        {
            myAnimator.SetBool("IsChasing", false);
            isChasing = false;
        }
    }

    public void Follow() //search theo keyword = "unity 2d follow object smoothly"
    {
        Vector3 distance = player.position - transform.position;

        if (distance.magnitude >= defaultDistanceTwoObject) // nếu khoảng cách giữa 2 obj lớn hơn cái đã cho thì mới cho đi lại gần
        {
            Vector3 targetPoint = player.position - distance.normalized * defaultDistanceTwoObject;
            targetPoint.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speedChase * Time.fixedDeltaTime);
        }
    }

    public void Flip()
    {
        //từ enemy tới player
        if (transform.position.x > player.position.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.x = 1f;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -1f;
            transform.localScale = localScale;
        }
    }
}
