using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void OnQuestBtnClick()
    {
        SceneManager.LoadScene("Quest");
    }

    public void OnMainBackBtnClick()
    {
        SceneManager.LoadScene("Main");
    }

    //양자리 퀘스트 버튼
    public void OnArisBtnClick()
    {
        AudioManager.GetInstance().ActSound();
        SceneManager.LoadScene("Aris");
    }

    // 황소자리 퀘스트 버튼
    public void OnTaurusBtnClick()
    {

        // 퀘스트 인덱스 확인
        if ( 90104 < DataController.GetInstance().GetQuestProcess())
        {
            AudioManager.GetInstance().ActSound();
            SceneManager.LoadScene("Taurus");
        }
    }

}
