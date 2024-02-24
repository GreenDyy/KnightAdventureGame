using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clip")]
    [Header("Background")]
    public AudioClip background;

    //skeleton
    [Header("Skeleton")]
    
    public AudioClip skeletonHurt1;
    public AudioClip skeletonHurt2;
    public AudioClip skeletonHurt3;
    public AudioClip skeletonDead;
    //player
    [Header("Player")]
    public AudioClip playerWalk;
    public AudioClip playerJump;
    public AudioClip playerDead;
    public AudioClip playerHurt;
    public AudioClip playerSlash;
    public AudioClip playerHeavySlash;
    public AudioClip playerThrowClover;
    //TinyPurple
    [Header("TinyPurple")]
    public AudioClip tpHurt;
    public AudioClip tpDead;
    //TinyPurple
    [Header("Slime")]
    public AudioClip slimeHurt;
    public AudioClip slimeDead;

    //Coin Clover
    [Header("CoinClover")]
    public AudioClip coin;

    private void Start()
    {
        instance = this;
        SFXSource.volume = 0.4f;
        musicSource.volume = 0.3f;
        musicSource.clip = background;
        musicSource.Play();
    }
    void Update()
    {
        //if (!musicSource.isPlaying)
        //{
        //    musicSource.Play();
        //}
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    //Note
    //Play thì nó sẽ hết mới lặp lại, OneShot thì ngắt ngang và chạy cái khác
}
