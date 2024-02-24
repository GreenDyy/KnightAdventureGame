using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDamageReceiver : DamageReceiver
{
    private void Awake()
    {
        healthbar = GetComponentInChildren<HealthBar>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        maxHP = 70;
        currentHP = maxHP;
        healthbar.SetMaxHeath(maxHP);
    }

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthbar.SetHealth(currentHP);
        myAnimator.SetTrigger("Hurt");
        AudioManager.instance.PlaySFX(AudioManager.instance.slimeHurt);
        if (IsDead())
            Dead();
    }
    public override void Dead()
    {
        Debug.Log(gameObject.name + " Die!");
        myAnimator.SetBool("IsDead", true);
        AudioManager.instance.PlaySFX(AudioManager.instance.slimeDead);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Invoke("Destroy", 1f);

    }

    public override void Destroy()
    {
        Vector2 spawnEffectPos = transform.position;
        spawnEffectPos.y -= 2;
        EffectManager.instance.SpawnVFX("DestroyEffect", spawnEffectPos, transform.rotation);
        Destroy(gameObject);
    }
}

