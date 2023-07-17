using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [Header("음량")]
    public float volumeBGM = 1f;
    public float volumeEffect = 1f;

    [Header("배경음악")]
    public AudioSource asBGM;
    public AudioClip BGMIntro;
    public AudioClip BGMMain;

    [Header("효과음")]
    public AudioSource asEffect;
    public AudioClip btn;
    public AudioClip trade;
    public AudioClip bottleHit;
    public AudioClip pot; //퐁당
    public AudioClip grind1;
    public AudioClip grind2;
    public AudioClip grind3;
    public AudioClip ingreHit;
    public AudioClip pestleHit1;
    public AudioClip pestleHit2;
    public AudioClip pestleHit3;
    public AudioClip potionFin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        asBGM.loop = true;
        asBGM.playOnAwake = true;
        PlayBGM("intro");
        asEffect.loop = false;
        asEffect.playOnAwake = false;
    }
    private void PlayBGM(string bgm)
    {
        switch (bgm)
        {
            case "intro":
                asBGM.clip = BGMIntro;
                break;
            case "main":
                asBGM.clip = BGMMain;
                break;
        }
        asBGM.Play();
    }
    public void PlayEffect(string effect)
    {
        switch (effect)
        {
            case "btn":
                asEffect.clip = btn;
                break;
            case "trade":
                asEffect.clip = trade;
                break;
            case "bottle":
                asEffect.clip = bottleHit;
                break;
            case "pot":
                asEffect.clip = pot;
                break;
            case "grind1":
                asEffect.clip = grind1;
                break;
            case "grind2":
                asEffect.clip = grind2;
                break;
            case "grind3":
                asEffect.clip = grind3;
                break;
            case "ingre":
                asEffect.clip = ingreHit;
                break;
            case "pestle1":
                asEffect.clip = pestleHit1;
                break;
            case "pestle2":
                asEffect.clip = pestleHit2;
                break;
            case "pestle3":
                asEffect.clip = pestleHit3;
                break;
            case "boil":
                asEffect.clip = potionFin;
                break;
        }
        asEffect.Play();
    }
}
