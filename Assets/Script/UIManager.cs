using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text goldDisplayer;
    public Text itemLimitDisplayer;

	void Update () {
        goldDisplayer.text = DataController.GetInstance().GetGold() + " 원";
        itemLimitDisplayer.text = DataController.GetInstance().GetItemCount() + " / " + DataController.GetInstance().GetItemLimit();
    }
}
