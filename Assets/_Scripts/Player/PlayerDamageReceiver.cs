using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    private void Awake() // nếu gọi lại Awake thì bắt buộc khởi tạo lại component
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        healthbar.SetMaxHeath(maxHP);
    }

    public override void TakeDamage(int damage) //Main nè, gọi hàm này thui, mấy cái dưới đều bổ trợ hàm này
    {
        currentHP -= damage;
        healthbar.SetHealth(currentHP);
        myAnimator.SetTrigger("Hurt");
        AudioManager.instance.PlaySFX(AudioManager.instance.playerHurt);
        //Play hurt animation
        if (IsDead())
        {
            Dead();
        }
    }
    public override void Dead()
    {
        Debug.Log("You Die!");
        myAnimator.SetBool("IsDead", true);
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlaySFX(AudioManager.instance.playerDead);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Invoke(nameof(ShowRestartButton), 1f);
    }

    public void ShowRestartButton()
    {
        Time.timeScale = 0f;
        UIManager.instance.btnRestart.SetActive(true);
    }
}
