using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDamageSender : MonoBehaviour
{
    public int stoneDamage = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            collision.gameObject.GetComponent<DamageReceiver>().TakeDamage(stoneDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
