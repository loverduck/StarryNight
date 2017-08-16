using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPopup : MonoBehaviour {

    private GameObject option;

    private void Awake()
    {
        option = GameObject.Find("Option Panel");
        option.SetActive(false);
    }

    public void EnterOption()
    {
        AudioManager.GetInstance().OptionSound();
        option.SetActive(true);
    }

    public void ExitOption()
    {
        option.SetActive(false);
    }
}
