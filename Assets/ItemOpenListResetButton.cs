using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpenListResetButton : MonoBehaviour
{
    public void OnClick()
    {
        DataController dataController = DataController.GetInstance();
        List<int> itemOpenList = dataController.itemOpenList;

        itemOpenList.Clear();

        dataController.SaveGameData(itemOpenList, dataController.itemOpenListPath);
    }
}