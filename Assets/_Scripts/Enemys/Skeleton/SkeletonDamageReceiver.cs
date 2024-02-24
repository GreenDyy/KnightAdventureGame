using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamageReceiver : DamageReceiver
{
    private List<AudioClip> hurtsSFX;
    private void Awake() // nếu gọi lại Awake thì bắt buộc khởi tạo lại component
    {
        healthbar = GetComponentInChildren<HealthBar>();
        myAnimator = GetComponent<Animator>();
        hurtsSFX = new List<AudioClip>();
    }

    private void Start()
    {
        maxHP = 125;
        currentHP = maxHP;
        healthbar.SetMaxHeath(maxHP);
    }

    public override void TakeDamage(int damage) //Main nè, gọi hàm này thui, mấy cái dưới đều bổ trợ hàm này
    {
        currentHP -= damage;
        healthbar.SetHealth(currentHP);
        myAnimator.SetTrigger("Hurt");
        PlayHurtSFX();
        if (IsDead())
            Dead();
    }
    public override void Dead()
    {
        Debug.Log(gameObject.name + " Die!");
        myAnimator.SetBool("IsDead", true);
        AudioManager.instance.PlaySFX(AudioManager.instance.skeletonDead);
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
    private void PlayHurtSFX()
    {
        hurtsSFX.Add(AudioManager.instance.skeletonHurt1);
        hurtsSFX.Add(AudioManager.instance.skeletonHurt2);
        hurtsSFX.Add(AudioManager.instance.skeletonHurt3);

        AudioClip randomHurt = hurtsSFX[Random.Range(0, hurtsSFX.Count)];
        AudioManager.instance.PlaySFX(randomHurt);
    }
}

