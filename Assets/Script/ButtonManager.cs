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

    public void OnArisBtnClick()
    {
        SceneManager.LoadScene("Aris");
    }

    public void OnTaurusBtnClick()
    {
        // 퀘스트 인덱스 확인
        if ( 90104 < DataController.GetInstance().GetQuestProcess())
        {
            SceneManager.LoadScene("Taurus");
        }
    }

}
