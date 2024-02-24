using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TPDamageSender : DamageSender
{
    public GameObject stone;

    private void Awake()
    {
        damageReceiver = GetComponent<TPDamageReceiver>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        attackPoint = transform.Find("AttackPointTP");
        triggerAttackPoint = transform.Find("TriggerAttackPoint");

        stone = GameObject.Find("Stone");
        //stone.SetActive(false);
    }

    private void Start()
    {
        damage = 10;
        attackRange = 0.8f;
        attackRate = 0.5f;
        attackDamage = 20;
    }

    public void RangeAttack()
    {
        stone.SetActive(false);
        GameObject stoneClone = Instantiate(stone);
        stoneClone.SetActive(true);
        stoneClone.transform.position = attackPoint.position; // ví trị spawn viên đá
        //đẩy đi
        Rigidbody2D stoneRb = stoneClone.GetComponent<Rigidbody2D>();
        if (transform.localScale.x == 1) //enemy mà bên phải thì ném ngược lại vì player ngược hướng
        {
            stoneRb.velocity = new Vector2(-10, stoneRb.velocity.y);
            stoneClone.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        else
        {
            stoneRb.velocity = new Vector2(10, stoneRb.velocity.y);
            stoneClone.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
    }
    public override void NormalAttack()
    {
        Invoke(nameof(DelayAnimation), 0.3f);
        Invoke(nameof(RangeAttack), 0.8f);
    }
 
    public override void PerformAttack()
    {
        float distanceEnemyToPlayer = Vector2.Distance(player.position, transform.position);
        float distanceEnemyToTriggerPointAttack = Vector2.Distance(transform.position, triggerAttackPoint.position);
        if (distanceEnemyToPlayer <= distanceEnemyToTriggerPointAttack && Time.time >= nextAttackTime)
        {
            NormalAttack();
            nextAttackTime = Time.time + 1f / attackRate; ;
        }
    }
}
