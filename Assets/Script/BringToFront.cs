using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringToFront : MonoBehaviour
{
    public GameObject itemListWindow;
    Transform windowTransform;

    private void Start()
    {
        windowTransform = itemListWindow.transform;
    }

    public void OnImgBtnClick()
    {
        ItemInfo itemInfo = GetComponent<ItemInfo>();

        windowTransform.Find("ItemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(itemInfo.imagePath);
        windowTransform.Find("ItemName").GetComponent<Text>().text = itemInfo.mtName;
        windowTransform.Find("ItemSort").GetComponent<Text>().text = itemInfo.group;
        windowTransform.Find("ItemGrade").GetComponent<Text>().text = itemInfo.grade;
        windowTransform.Find("ItemCost").GetComponent<Text>().text = itemInfo.sellPrice.ToString();
        windowTransform.Find("ItemText").GetComponent<Text>().text = itemInfo.description;

        windowTransform.SetAsLastSibling();
    }

    public void OnExitBtnClick()
    {
        windowTransform.SetAsFirstSibling();
    }
}