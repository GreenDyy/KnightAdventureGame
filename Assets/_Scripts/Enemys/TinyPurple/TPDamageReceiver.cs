using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPDamageReceiver : DamageReceiver
{
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        healthbar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        healthbar.SetMaxHeath(maxHP);
    }

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthbar.SetHealth(currentHP);
        myAnimator.SetTrigger("Hurt");
        AudioManager.instance.PlaySFX(AudioManager.instance.tpHurt);
        if (IsDead())
            Dead();
    }
    public override void Dead()
    {
        Debug.Log(gameObject.name + " Die!");
        myAnimator.SetBool("IsDead", true);
        AudioManager.instance.PlaySFX(AudioManager.instance.tpDead);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Invoke(nameof(DestroyEffect), 0.2f);
        Invoke(nameof(Destroy), 1f);
    }
}
