﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    public Image logo;
    public Button start;
    public Text loading;

    
    private void Awake()
    {
        Color textColor = new Vector4(1, 1, 1, 0);
        loading.color = textColor;
        loading.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    // 로딩 완료 시 start 버튼 띄우기
    IEnumerator FadeOut()
    {
        for (float i = 0f; i <= 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            Color color = new Vector4(1, 1, 1, i);
            logo.color = color;
            
        }
        yield return new WaitForSeconds(0.2f);
        loading.text = "Loading...";
        loading.color = new Vector4(1, 1, 1, 1);
        while (!DataController.GetInstance().GetloadingFinish())
        {
            yield return new WaitForSeconds(0.2f);
            loading.text = "Loading.";
            yield return new WaitForSeconds(0.2f);
            loading.text = "Loading..";
            yield return new WaitForSeconds(0.2f);
            loading.text = "Loading...";
        }
        loading.text = "Start";
        loading.enabled = true;
    }

    // start 클릭 시 메인화면 이동
    public void Click()
    {
        SceneManager.LoadScene("Main");
    }

}
