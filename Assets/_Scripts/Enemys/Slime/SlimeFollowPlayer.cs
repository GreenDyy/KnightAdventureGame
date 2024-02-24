using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFollowPlayer : FollowPlayer
{
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        damageReceiver = GetComponent<SlimeDamageReceiver>();
        damageSender = GetComponent<SlimeDamageSender>();
    }

    void FixedUpdate()
    {
        isChasing = false;
    }
}
