using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 0.8f;
    public LayerMask playerLayers;
    public float attackRate = 1f;
    protected float nextAttackTime = 0f;
    public int attackDamage = 20;

    public DamageReceiver damageReceiver;
    public Transform player;
    public Transform attackPoint;
    public Transform triggerAttackPoint;
    public Animator animator;
    public bool isAttacking = false;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        animator = GetComponent<Animator>();
        playerLayers = LayerMask.NameToLayer("Player");
        player = GameObject.Find("Player").transform;
        attackPoint = transform.Find("AttackPoint");
        triggerAttackPoint = transform.Find("TriggerAttack");
    }

    private void Update()
    {
        if (!damageReceiver.IsDead())
            PerformAttack();
        else
        {
            CancelInvoke(nameof(DelayAnimation));
            CancelInvoke(nameof(DelayTakeDame));
            CancelInvoke(nameof(StopAttack));
        }
    }

    public virtual void NormalAttack()
    {
        Invoke(nameof(DelayAnimation), 0.3f);
        Invoke(nameof(DelayTakeDame), 0.5f);
        isAttacking = true;
        Invoke(nameof(StopAttack), 1f);
    }
    public virtual void DelayAnimation()
    {
        animator.SetTrigger("Attack");
        AudioManager.instance.PlaySFX(AudioManager.instance.playerSlash);
    }
    public virtual void DelayTakeDame()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<DamageReceiver>().TakeDamage(attackDamage);
            Debug.Log(gameObject.name + " vừa thực hiện NormalAttack gây " + attackDamage + " sát thương lên bạn");
        }
    }
    public virtual void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
        }
    }
    public virtual void PerformAttack()
    {
        float distanceEnemyToPlayer = Vector2.Distance(player.position, transform.position);
        float distanceEnemyToTriggerPointAttack = Vector2.Distance(transform.position, triggerAttackPoint.position);
        if (distanceEnemyToPlayer <= distanceEnemyToTriggerPointAttack && Time.time >= nextAttackTime)
        {
            NormalAttack();
            nextAttackTime = Time.time + 1f / attackRate; ;
        }
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    PlayerDamageReceiver playerDamageReceiver = collision.gameObject.GetComponent<PlayerDamageReceiver>();
    //    if (collision.gameObject.layer == playerLayers)
    //    {
    //        playerDamageReceiver.TakeDamage(damage);
    //    }
    //} 
}
