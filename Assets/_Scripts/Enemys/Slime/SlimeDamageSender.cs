using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeDamageSender : DamageSender
{
    private void Awake()
    {
        damageReceiver = GetComponent<SlimeDamageReceiver>();
        animator = GetComponent<Animator>();
        playerLayers = LayerMask.NameToLayer("Player");
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        damage = 10;
    }

    private void Update()
    {
        //phải ghi lại cái này chứ k nó chạy bên cha mà cha lại k dùng cho slime
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamageReceiver playerDamageReceiver = collision.gameObject.GetComponent<PlayerDamageReceiver>();
        if (collision.gameObject.layer == playerLayers)
        {
            playerDamageReceiver.TakeDamage(damage);
        }
    }
}
