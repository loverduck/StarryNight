﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemTimer1 : MonoBehaviour
{
    public GameObject prefab;
    public Text timeDisplayer; // 남은 시간 표시
    public Image img;
    public Button btn;
    float cooltime = 300.0f; // 쿨타임 -> 타이머 쿨타임 업그레이드 추가 시 datacontroller에서 가져오는 걸로 수정필요. itemtimer2,3도 마찬가지
    public bool disableOnStart = false;
    private int sec, sec_1, sec_10, min;
    private ItemDictionary itemDic;

    public Button combineButton;

    private void Awake()
    {
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
    }

    void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();

        if (btn == null)
            btn = gameObject.GetComponent<Button>();
    }

    // 시간당 게이지 채우기, 남은 시간 표시
    void Update()
    {
        if (DataController.GetInstance().GetLeftTimer1() > 0)
        {
            btn.enabled = false;
            sec = (int)DataController.GetInstance().GetLeftTimer1() % 60;
            sec_10 = sec / 10;
            sec_1 = sec % 10;
            min = (int)DataController.GetInstance().GetLeftTimer1() / 60;
            timeDisplayer.text = min + ":" + sec_10 + sec_1;

            if (DataController.GetInstance().GetLeftTimer1() < 0)
            {
                DataController.GetInstance().SetLeftTimer1(0);

                if (btn)
                {
                    btn.enabled = true;
                }
            }

            float ratio = 1.0f - (DataController.GetInstance().GetLeftTimer1() / cooltime);

            if (img)
                img.fillAmount = ratio;
        }
        else
        {
            timeDisplayer.text = "0:00";
            img.fillAmount = 1.0f;
            
            DataController.GetInstance().SetLeftTimer1(0);

            if (btn)
            {
                btn.enabled = true;
            }
        }
    }

    // 쿨타임 시간 지났는지 확인
    public bool CheckCooltime()
    {
        if (DataController.GetInstance().GetLeftTimer1() > 0)
            return false;
        else
            return true;
    }

    public void ResetCooltime()
    {
        if (btn) // 버튼 활성화 시
        {
            if (DataController.GetInstance().GetItemCount() >= DataController.GetInstance().GetItemLimit()) // 아이템 갯수 제한
            {
                Debug.Log("아이템 상자가 꽉 찼어요");
                return;
            }

            // 세트 아이템 랜덤 생성
            int id = Random.Range(4001, 4059);

            while (id % 5 == 0)
            {
                id = Random.Range(4001, 4059);
            }

            CreateSetItem(id);
            AudioManager.GetInstance().ItemSound();

            DataController.GetInstance().SetLeftTimer1(cooltime);
            btn.enabled = false;

            DataController.GetInstance().AddItemCount();
        }
    }

    private void CreateSetItem(int productID)
    {
        GameObject setItem = Instantiate(prefab, new Vector3(-670, 772, -3), Quaternion.identity);

        DataController.GetInstance().InsertItem(productID, 1);

        SetItemInfo setItemInfo = ItemDictionary.GetInstance().CheckSetItemCombine(productID);

        if (setItemInfo.result != 0)
        {
            combineButton.gameObject.SetActive(true);
            combineButton.onClick.AddListener(() => OnClick(setItemInfo));
        }

        ItemInfo itemInfo = setItem.GetComponent<ItemInfo>();
        ItemInfo findItemInfo = itemDic.findDic[productID];

        itemInfo.index = productID;
        itemInfo.mtName = findItemInfo.mtName;
        itemInfo.group = findItemInfo.group;
        itemInfo.grade = findItemInfo.grade;
        itemInfo.sellPrice = findItemInfo.sellPrice;
        itemInfo.description = findItemInfo.description;
        itemInfo.imagePath = findItemInfo.imagePath;

        setItem.GetComponent<BoxCollider2D>().isTrigger = false;
        setItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);
    }

    void OnClick(SetItemInfo setItemInfo)
    {
        DataController dataController = DataController.GetInstance();

        dataController.DeleteItem(setItemInfo.index1);
        dataController.DeleteItem(setItemInfo.index2);
        dataController.DeleteItem(setItemInfo.index3);
        dataController.DeleteItem(setItemInfo.index4);

        dataController.InsertItem(setItemInfo.result, 1);

        SceneManager.LoadScene("Main");
    }
}