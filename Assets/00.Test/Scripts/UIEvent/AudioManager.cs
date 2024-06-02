using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    public static AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    public static AudioSource[] sfxPlayers;
    int channelIndex;// 맨 마지막에 실행된 오디오 플레이어

    public enum sfx{damage, damageTwo, fail, hit, hitTwo, MonsterDie, coin, Jump, re, dash, run, mHit, pHit, W, mHitI, BGM, Death, Typing}

    void Awake() 
    {
        instance = this;
        Init();
            
    }

    void Init()
    {
        //배경음 플레이어 초기화 
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;


        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index=0; index < sfxPlayers.Length; index++) {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    
    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else 
        {
            bgmPlayer.Stop(); 
        }
    }
    

    public void PlaySfx(sfx sfx)
    {
        for(int index=0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if(sfxPlayers[loopIndex].isPlaying)//지금 실행되고 있는 효과음 있는지
                continue;  //다음 플레이어 재생

            //  같은 효과음 시리즈 중 랜덤으로 하고 싶을떄 
            /*int ranIndex = 0;
            if(sfx == sfx.hit || sfx == sfx.damage) 
            {
                ranIndex = Random.Range(0, 2);
            }*/ 

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];

            //  같은 효과음 시리즈 중 랜덤으로 하고 싶을떄
            // sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];

            sfxPlayers[loopIndex].Play();
            break;
        }
    
    //AudioManager.instance.PlaySfx(AudioManager.sfx.);
    //내가 오디오 쓰고 싶은 곳에 이 코드 작성하면 됌 (함수안에)
    }

   /* public void StopSfx(sfx sfx)
{
    for(int index = 0; index < sfxPlayers.Length; index++)
    {
        if(sfxPlayers[index].clip == sfxClips[(int)sfx] && sfxPlayers[index].isPlaying)
        {
            sfxPlayers[index].Stop();
            break;
        }
    }
}*/

    
}


