using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloverCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.coin);
            Destroy(gameObject);
            CloverCounter.instance.IncreaseClovers(1);
            PlayerCombat.instance.amountOfClover++;
        }
    }
}
