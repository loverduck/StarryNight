using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

    public Button bgm;
    public Button effect;
    public Button voice;

    public Text bgmDisplayer;
    public Text effDisplayer;
    public Text voiceDisplayer;

    private void Awake()
    {

        if (AudioManager.GetInstance().GetBGMAlive() == 1)
        {
            bgm.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            bgmDisplayer.text = "ON";
        }
        else
        {
            AudioManager.GetInstance().BGMOff();
            bgm.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            bgmDisplayer.text = "OFF";
        }

        if (AudioManager.GetInstance().GetEffAlive() == 1)
        {
            effect.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            effDisplayer.text = "ON";
        }
        else
        {
            effect.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            effDisplayer.text = "OFF";
        }

        if (AudioManager.GetInstance().GetVoiceAlive() == 1)
        {
            voice.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            voiceDisplayer.text = "ON";
        }
        else
        {
            voice.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            voiceDisplayer.text = "OFF";
        }
    }

    public void BGMButton()
    {
        if (AudioManager.GetInstance().GetBGMAlive() == 1)
        {
            AudioManager.GetInstance().BGMOff();
            AudioManager.GetInstance().SetBGMAlive(0);
            bgm.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            bgmDisplayer.text = "OFF";
        }
        else
        {
            AudioManager.GetInstance().BGMOn();
            AudioManager.GetInstance().SetBGMAlive(1);
            bgm.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            bgmDisplayer.text = "ON";
        }
    }

    public void EffectButton()
    {
        if (AudioManager.GetInstance().GetEffAlive() == 1)
        {
            AudioManager.GetInstance().SetEffAlive(0);
            effect.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            effDisplayer.text = "OFF";
        }
        else
        {
            AudioManager.GetInstance().SetEffAlive(1);
            effect.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            effDisplayer.text = "ON";
        }
    }

    public void VoiceButton()
    {
        if (AudioManager.GetInstance().GetVoiceAlive() == 1)
        {
            AudioManager.GetInstance().SetVoiceAlive(0);
            voice.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/pull");
            voiceDisplayer.text = "OFF";
        }
        else
        {
            AudioManager.GetInstance().SetVoiceAlive(1);
            voice.GetComponent<Image>().sprite = Resources.Load<Sprite>("optionImg/push");
            voiceDisplayer.text = "ON";
        }
    }

}
