using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public HealthBar healthbar;
    public Animator myAnimator;

    private void Reset()
    {
        healthbar = GetComponentInChildren<HealthBar>(); 
    }

    void Awake()
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

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthbar.SetHealth(currentHP);
        myAnimator.SetTrigger("Hurt");

        if (IsDead())
        {
            Dead();
            PlayerCombat.instance.amountOfClover += 1;
        }
    }
    public virtual void Dead()
    {
        Debug.Log(gameObject.name + " Die!");
        myAnimator.SetBool("IsDead", true);
        AudioManager.instance.PlaySFX(AudioManager.instance.skeletonDead);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Invoke(nameof(DestroyEffect), 0.2f);
        Invoke(nameof(Destroy), 1f);
    }

    public virtual void DestroyEffect()
    {
        EffectManager.instance.SpawnVFX("DestroyEffect", transform.position, transform.rotation);
    }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
    public bool IsDead()
    {
        return currentHP <= 0;
    }
}
