﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkStar : MonoBehaviour
{
    public int questIndex;

    // 퀘스트 별 버튼
    private Button btn;
    // 퀘스트 별 버튼 이미지
    private Image btnImg;

    private Color btnColor_d;
    private Color btnColor_a;

    private QuestDictionary questDic;
    private ItemDictionary itemDic;
    private UpgradeDictionary upgradeDic;

    // 현재 퀘스트
    private QuestInfo currentQuest;

    // 별 깜박임 활성화 유무
    private bool blinkAlive;

    private void Awake()
    {
        questDic = GameObject.FindWithTag("DataController").GetComponent<QuestDictionary>();
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
        upgradeDic = GameObject.FindWithTag("DataController").GetComponent<UpgradeDictionary>();

        btn = gameObject.GetComponent<Button>();
        btnImg = gameObject.GetComponent<Image>();
        btn.enabled = false;

        btnColor_d.a = 0;
        btnColor_a.a = 255;
        btnColor_a.r = 255;
        btnColor_a.g = 255;
        btnColor_a.b = 255;

        blinkAlive = true;

        LoadBtnInfo();

        // 퀘스트 진행도 확인 후 별 sprite 설정 및 버튼 활성화
        if (questIndex > DataController.GetInstance().GetQuestProcess()) // 진행 전 퀘스트
        {
            btn.enabled = false;
            btnImg.sprite = Resources.Load<Sprite>("questImg/quest_uncomplete");
        }
        else
        {
            btn.enabled = true;
            btnImg.sprite = Resources.Load<Sprite>("questImg/quest_complete");

            if (questIndex == DataController.GetInstance().GetQuestProcess()) // 진행중인 퀘스트
            {
                btnImg.sprite = Resources.Load<Sprite>("questImg/quest_ongoing");
                StartCoroutine(Blink());
            }
        }
    }

    // 퀘스트 진행 상태에 따른 별 이미지 가져오기
    private void Update()
    {

        if (questIndex > DataController.GetInstance().GetQuestProcess())
        {
            btn.enabled = false;
            btnImg.sprite = Resources.Load<Sprite>("questImg/quest_uncomplete");
        }
        else
        {
            btn.enabled = true;
            btnImg.sprite = Resources.Load<Sprite>("questImg/quest_complete");

            if (questIndex == DataController.GetInstance().GetQuestProcess())
            {
                btnImg.sprite = Resources.Load<Sprite>("questImg/quest_ongoing");
            }
        }
    }

    private void LoadBtnInfo()
    {
        QuestInfo questInfo = btn.GetComponent<QuestInfo>();
        QuestInfo findQuestInfo = questDic.findQuestDic[questIndex];

        questInfo.index = questIndex;
        questInfo.act = findQuestInfo.act;
        questInfo.title = findQuestInfo.title;
        questInfo.content = findQuestInfo.content;
        questInfo.termsItem = findQuestInfo.termsItem;
        questInfo.termsCount = findQuestInfo.termsCount;
        questInfo.reward = findQuestInfo.reward;
        questInfo.rewardCount = findQuestInfo.rewardCount;
    }

    // 퀘스트 정보 확인 및 퀘스트 완료 보상 지급
    public void OnClick()
    {
        AudioManager.GetInstance().QuestStarSound();
        currentQuest = questDic.FindQuest(DataController.GetInstance().GetQuestProcess());

        // 진행중인 퀘스트 조건 아이템의 인덱스
        int checkItemIndex = currentQuest.termsItem;
        // 진행중인 퀘스트 조건을 만족하는 아이템 개수 
        int checkItemCount = currentQuest.termsCount;
        // 현재 가지고 있는 조건 아이템 갯수
        int currentItemNum = 0;

        // 조건이 [골드/업그레이드/아이템] 인지 확인
        if (checkItemIndex == 9999) // 골드일 때
        {
            currentItemNum = (int)DataController.GetInstance().GetGold();
        }
        else if (checkItemIndex > 50000) // 업그레이드일 때
        {
            currentItemNum = DataController.GetInstance().CheckUpgradeLevel(checkItemIndex);
        }
        else // 아이템일 때
        {
            if (currentQuest.index == 90101) // 퀘스트인덱스 90101의 경우
            {
                for (int i = 1001; i < 1004; i++)
                {
                    currentItemNum += DataController.GetInstance().GetItemNum(i);
                }
            }
            else if (currentQuest.index == 90102) // 퀘스트인덱스 90102의 경우
            {
                for (int i = 1004; i < 1007; i++)
                {
                    currentItemNum += DataController.GetInstance().GetItemNum(i);
                }
            }
            else if (currentQuest.index == 90103) // 퀘스트인덱스 90103의 경우
            {
                for (int i = 2001; i < 2007; i++)
                {
                    currentItemNum += DataController.GetInstance().GetItemNum(i);
                }
            }
            else if (currentQuest.index == 90104) // 퀘스트인덱스 90104의 경우
            {
                for (int i = 3001; i < 3019; i++)
                {
                    currentItemNum += DataController.GetInstance().GetItemNum(i);
                }
            }
            else
            {
                currentItemNum = DataController.GetInstance().GetItemNum(checkItemIndex);
            }

        }

        // 조건 아이템의 갯수 확인 및 보상 지급
        if (checkItemCount <= currentItemNum)
        {
            if (currentQuest.reward == 9999) // 보상이 골드일 때
            {
                DataController.GetInstance().AddGold((ulong)currentQuest.rewardCount);
            }
            else
            {
                // 아이템 인벤토리가 꽉 차있는지 확인
                if (DataController.GetInstance().GetItemCount() < DataController.GetInstance().GetItemLimit())
                {
                    if (currentQuest.reward > 50000) // 보상이 업그레이드 오픈일 때
                    {
                        if (currentQuest.termsItem == 9999) // 조건이 골드일 때 골드 감소
                        {
                            DataController.GetInstance().SubGold((ulong)currentQuest.termsCount);
                        }

                        DataController.GetInstance().SetMaxUpgradeLevel(currentQuest.reward);
                    }
                    else
                    {
                        // 조건이 골드일 경우 골드 감소
                        if (currentQuest.termsItem == 9999)
                        {
                            DataController.GetInstance().SubGold((ulong)currentQuest.termsCount);
                        }

                        DataController.GetInstance().InsertItem(currentQuest.reward, currentQuest.rewardCount);
                        DataController.GetInstance().AddItemCount();
                    }

                    DataController.GetInstance().NextQuest();
                    blinkAlive = false;

                    // 다음 퀘스트 별 반짝 거리기
                    if (DataController.GetInstance().GetQuestProcess() < 90105) // 양자리 일 때
                    {

                        BlinkStar nextStar = GameObject.Find("Aris_" + DataController.GetInstance().GetQuestProcess()).GetComponent<BlinkStar>();
                        nextStar.BlingBling();
                    }
                    else if (DataController.GetInstance().GetQuestProcess() > 90105 && DataController.GetInstance().GetQuestProcess() < 90124) // 황소자리일 때
                    {
                        BlinkStar nextStar = GameObject.Find("Taurus_" + DataController.GetInstance().GetQuestProcess()).GetComponent<BlinkStar>();
                        nextStar.BlingBling();
                    }
                }
            }         
        }

        // 퀘스트 제목 출력
        GameObject.Find("Name Displayer").GetComponent<Text>().text = btn.GetComponent<QuestInfo>().title;

        // 퀘스트 진행상태 출력
        if (questIndex == 90101)
        {
            int questItemNum = 0;
            for (int i = 1001; i < 1004; i++)
            {
                questItemNum += DataController.GetInstance().GetItemNum(i);
            }

            if (questIndex < DataController.GetInstance().GetQuestProcess())
            {
                //GameObject.Find("Progress Displayer").GetComponent<Text>().text = "별의 원석 " + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
            }
            else
            {
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "별의 원석 " + questItemNum + "/" + btn.GetComponent<QuestInfo>().termsCount;
            }
        }
        else if (questIndex == 90102)
        {
            int questItemNum = 0;
            for (int i = 1004; i < 1007; i++)
            {
                questItemNum += DataController.GetInstance().GetItemNum(i);
            }
            if (questIndex < DataController.GetInstance().GetQuestProcess())
            {
                //GameObject.Find("Progress Displayer").GetComponent<Text>().text = "별의 파편 " + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
            }
            else
            {
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "별의 파편 " + questItemNum + "/" + btn.GetComponent<QuestInfo>().termsCount;
            }
        }
        else if (questIndex == 90103)
        {
            int questItemNum = 0;
            for (int i = 2001; i < 2007; i++)
            {
                questItemNum += DataController.GetInstance().GetItemNum(i);
            }

            if (questIndex < DataController.GetInstance().GetQuestProcess())
            {
                //GameObject.Find("Progress Displayer").GetComponent<Text>().text = "재료 아이템 " + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
            }
            else
            {
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "재료 아이템 " + questItemNum + "/" + btn.GetComponent<QuestInfo>().termsCount;
            }
        }
        else if (questIndex == 90104)
        {
            int questItemNum = 0;

            for (int i = 3001; i < 3019; i++)
            {
                questItemNum += DataController.GetInstance().GetItemNum(i);
            }

            if (questIndex < DataController.GetInstance().GetQuestProcess())
            {
                //GameObject.Find("Progress Displayer").GetComponent<Text>().text = "레벨 1 조합 아이템 " + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
            }
            else
            {
                GameObject.Find("Progress Displayer").GetComponent<Text>().text = "레벨 1 조합 아이템 " + questItemNum + "/" + btn.GetComponent<QuestInfo>().termsCount;
            }
        }
        else
        {
            int termItemIndex = btn.GetComponent<QuestInfo>().termsItem;
            if (termItemIndex == 9999) // 조건이 골드일 때
            {
                if (questIndex < DataController.GetInstance().GetQuestProcess())
                {
                    //GameObject.Find("Progress Displayer").GetComponent<Text>().text = "골드 " + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
                }
                else
                {
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = "골드 " + DataController.GetInstance().GetGold() + "/" + btn.GetComponent<QuestInfo>().termsCount;
                }
            }
            else if (termItemIndex > 50000) // 조건이 업그레이드일 때
            {
                if (questIndex < DataController.GetInstance().GetQuestProcess())
                {
                    //GameObject.Find("Progress Displayer").GetComponent<Text>().text = upgradeDic.FindUpgrade(termItemIndex).name + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
                }
                else
                {
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = upgradeDic.FindUpgrade(termItemIndex).name + DataController.GetInstance().CheckUpgradeLevel(termItemIndex) + "/" + btn.GetComponent<QuestInfo>().termsCount;
                }
            }
            else
            {
                if (questIndex < DataController.GetInstance().GetQuestProcess())
                {
                    //GameObject.Find("Progress Displayer").GetComponent<Text>().text = itemDic.findDic[termItemIndex].mtName + btn.GetComponent<QuestInfo>().termsCount + "/" + btn.GetComponent<QuestInfo>().termsCount;
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = "퀘스트 완료";
                }
                else
                {
                    GameObject.Find("Progress Displayer").GetComponent<Text>().text = itemDic.findDic[termItemIndex].mtName + DataController.GetInstance().GetItemNum(termItemIndex) + "/" + btn.GetComponent<QuestInfo>().termsCount;
                }
            }
        }

        // 퀘스트 내용 출력
        GameObject.Find("Content Displayer").GetComponent<Text>().text = btn.GetComponent<QuestInfo>().content;

        // 퀘스트 보상 출력
        int rewardIndex = btn.GetComponent<QuestInfo>().reward;
        if (rewardIndex == 9999)
        {
            GameObject.Find("Reward Displayer").GetComponent<Text>().text = "골드 " + btn.GetComponent<QuestInfo>().rewardCount;
        }
        else if (rewardIndex > 50000)
        {
            GameObject.Find("Reward Displayer").GetComponent<Text>().text = upgradeDic.FindUpgrade(rewardIndex).name + " Lv." + btn.GetComponent<QuestInfo>().rewardCount + " 오픈";
        }
        else
        {
            GameObject.Find("Reward Displayer").GetComponent<Text>().text = itemDic.findDic[btn.GetComponent<QuestInfo>().reward].mtName + " " + btn.GetComponent<QuestInfo>().rewardCount;
        }
    }

    // 진행중인 별 깜박임
    IEnumerator Blink()
    {
        while (blinkAlive)
        {
            yield return new WaitForSeconds(0.5f);
            btnImg.color = btnColor_d;
            yield return new WaitForSeconds(0.5f);
            btnImg.color = btnColor_a;
        }
    }

    public void BlingBling()
    {
        StartCoroutine(Blink());
    }
}