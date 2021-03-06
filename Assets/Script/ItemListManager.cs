﻿using UnityEngine;
using UnityEngine.UI;

public class ItemListManager : MonoBehaviour
{
    int starIdxStart, materialIdxStart, combineIdxStart, setIdxStart;
    int starIdxMax, materialIdxMax, combineIdxMax, setIdxMax;

    ItemDictionary itemDic;

    public GameObject panel;
    public GameObject itemInfoPanel;

    Transform starContentPanel, materialContentPanel, combineContentPanel, setContentPanel;

    private static ItemListManager instance;

    public static ItemListManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ItemListManager>();

            if (instance == null)
            {
                GameObject container = new GameObject("ItemListManager");
                instance = container.AddComponent<ItemListManager>();
            }
        }

        return instance;
    }

    private void Awake()
    {
        itemDic = DataController.GetInstance().GetComponent<ItemDictionary>();

        starIdxStart = 1000;
        materialIdxStart = 2000;
        combineIdxStart = 3000;
        setIdxStart = 4000;

        starIdxMax = starIdxStart + itemDic.starNum;
        materialIdxMax = materialIdxStart + itemDic.materialNum;
        combineIdxMax = combineIdxStart + itemDic.combineNum;
        setIdxMax = setIdxStart + itemDic.setNum;

        starContentPanel = GameObject.Find("StarContentPanel").transform;
        materialContentPanel = GameObject.Find("MaterialContentPanel").transform;
        combineContentPanel = GameObject.Find("CombineContentPanel").transform;
        setContentPanel = GameObject.Find("SetContentPanel").transform;
    }

    private void Start()
    {
        for (int idx = starIdxStart + 1; idx <= starIdxMax; idx++)
        {
            AddItemButton(idx, starContentPanel);
        }

        for (int idx = materialIdxStart + 1; idx <= materialIdxMax; idx++)
        {
            AddItemButton(idx, materialContentPanel);
        }

        for (int idx = combineIdxStart + 1; idx <= combineIdxMax; idx++)
        {
            AddItemButton(idx, combineContentPanel);
        }

        for (int idx = setIdxStart + 1; idx <= setIdxMax; idx++)
        {
            AddItemButton(idx, setContentPanel);
        }
    }

    void AddItemButton(int idx, Transform tf)
    {
        GameObject itemListPanel = Instantiate(panel);
        Button itemBtn = itemListPanel.GetComponentInChildren<Button>();
        Image itemLock = itemListPanel.transform.Find("ItemLock").GetComponent<Image>();

        ItemInfo itemInfo = itemBtn.GetComponent<ItemInfo>();

        ItemInfo findItemInfo = itemDic.findDic[idx];

        itemInfo.index = idx;
        itemInfo.mtName = findItemInfo.mtName;
        itemInfo.group = findItemInfo.group;
        itemInfo.grade = findItemInfo.grade;
        itemInfo.sellPrice = findItemInfo.sellPrice;
        itemInfo.description = findItemInfo.description;
        itemInfo.imagePath = findItemInfo.imagePath;

        itemListPanel.transform.SetParent(tf);
        itemBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(itemInfo.imagePath);

        if (DataController.GetInstance().itemOpenList.Contains(itemInfo.index))
        {
            itemLock.gameObject.SetActive(false);

            ColorBlock btnColors = itemBtn.colors;

            btnColors.normalColor = Color.white;
            btnColors.highlightedColor = Color.white;
            btnColors.pressedColor = Color.white;

            itemBtn.colors = btnColors;

            itemBtn.onClick.AddListener(() => ShowWindow(itemInfo));
        }
    }

    public void ShowWindow(ItemInfo itemInfo)
    {
        itemInfoPanel.SetActive(true);

        ItemInfoWindow infoWindow = itemInfoPanel.transform.Find("ItemInfoWindow").GetComponent<ItemInfoWindow>();

        infoWindow.gameObject.SetActive(true);

        infoWindow.itemImg.sprite = Resources.Load<Sprite>(itemInfo.imagePath);
        infoWindow.itemName.text = itemInfo.mtName;
        infoWindow.itemSort.text = itemInfo.group;
        infoWindow.itemGrade.text = itemInfo.grade;
        infoWindow.itemCost.text = "판매 가격 : " + itemInfo.sellPrice.ToString();
        infoWindow.itemText.text = itemInfo.description;
    }
}