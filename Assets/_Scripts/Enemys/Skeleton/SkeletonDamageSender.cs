using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonDamageSender : DamageSender
{
    private void Awake()
    {
        damageReceiver = GetComponent<SkeletonDamageReceiver>();
        animator = GetComponent<Animator>();
        playerLayers = LayerMask.GetMask("Player");
        player = GameObject.Find("Player").transform;
        attackPoint = transform.Find("AttackPointSkeleton");
        triggerAttackPoint = transform.Find("TriggerAttackPoint");
    }

    private void Start()
    {
        damage = 10;
        attackRange = 0.8f;
        attackRate = 1f;
        attackDamage = 20;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
