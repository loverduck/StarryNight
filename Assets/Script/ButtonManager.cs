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
}
