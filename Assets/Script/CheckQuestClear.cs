using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuestClear : MonoBehaviour {

    private GameObject arisClear;
    private GameObject taurusClear;

    private void Awake()
    {
        arisClear = GameObject.Find("Aris Clear");
        taurusClear = GameObject.Find("Taurus Clear");

        arisClear.SetActive(false);
        taurusClear.SetActive(false);

        if (DataController.GetInstance().GetQuestProcess() > 90104)
        {
            arisClear.SetActive(true);
        }

        if (DataController.GetInstance().GetQuestProcess() > 90123)
        {
            taurusClear.SetActive(true);
        }
    }

}
