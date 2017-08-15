using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveDicResetButton : MonoBehaviour
{
    public void OnClick()
    {
        DataController dataController = DataController.GetInstance();
        Dictionary<int, int> haveDic = dataController.haveDic;

        haveDic.Clear();

        dataController.SaveGameData(haveDic, dataController.haveDicPath);
    }
}