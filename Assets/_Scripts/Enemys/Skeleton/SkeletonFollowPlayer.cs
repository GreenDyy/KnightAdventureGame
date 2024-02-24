using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonFollowPlayer : FollowPlayer
{
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        damageReceiver = GetComponent<SkeletonDamageReceiver>();
        damageSender = GetComponent<SkeletonDamageSender>();
    }
}
