using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverDamageSender : MonoBehaviour
{
    public int cloverDamage = 15;
    private void Start()
    {
        Invoke(nameof(SelfDestroy), 4f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //index của Layer Enemy là 6
        {
            collision.gameObject.GetComponent<DamageReceiver>().TakeDamage(cloverDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
