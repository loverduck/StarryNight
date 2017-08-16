using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    AudioSource myAudio;

    private int bgmAlive;
    private int effAlive;
    private int voiceAlive;

    public AudioClip click;
    public AudioClip sale;
    public AudioClip act;
    public AudioClip option;
    public AudioClip item;
    public AudioClip mix;
    public AudioClip questStar;
    private AudioClip voice;

    public static AudioManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<AudioManager>();

            if (instance == null)
            {
                GameObject container = new GameObject("AudioManager");
                instance = container.AddComponent<AudioManager>();
            }
        }

        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bgmAlive = PlayerPrefs.GetInt("BGM", 0);
        effAlive = PlayerPrefs.GetInt("Effect", 0);
        voiceAlive = PlayerPrefs.GetInt("Voice", 1);
        myAudio = gameObject.GetComponent<AudioSource>();
    }

    public int GetBGMAlive()
    {
        return bgmAlive;
    }

    public void SetBGMAlive(int alive)
    {
        bgmAlive = alive;
        PlayerPrefs.SetInt("BGM", bgmAlive);
    }

    public int GetEffAlive()
    {
        return effAlive;
    }

    public void SetEffAlive(int alive)
    {
        effAlive = alive;
        PlayerPrefs.SetInt("Effect", effAlive);
    }

    public int GetVoiceAlive()
    {
        return voiceAlive;
    }

    public void SetVoiceAlive(int alive)
    {
        voiceAlive = alive;
        PlayerPrefs.SetInt("Voice", voiceAlive);
    }

    public void BGMOff()
    {
        myAudio.Stop();
    }

    public void BGMOn()
    {
        myAudio.Play();
    }

    public void ClickSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(click);
        }
    }

    public void SaleSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(sale);
        }
    }

    public void ActSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(act);
        }
    }

    public void OptionSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(option);
        }
    }

    public void ItemSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(item);
        }
    }

    public void MixSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(mix);
        }
    }

    public void QuestStarSound()
    {
        if (effAlive == 1)
        {
            myAudio.PlayOneShot(questStar);
        }
    }

    public void VoiceSound()
    {
        if (voiceAlive == 1)
        {
            int num = Random.Range(1,7);
            AudioClip voice = Resources.Load<AudioClip>("audio/voice_" + num);
            myAudio.PlayOneShot(voice);
        }
    }

}
