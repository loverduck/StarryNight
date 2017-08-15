using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkStar : MonoBehaviour
{

    public int questIndex;

    private Button btn;
    private Image btnImg;

    private Color btnColor_d;
    private Color btnColor_a;

    private QuestDictionary questDic;
    private ItemDictionary itemDic;
    private UpgradeDictionary upgradeDic;

    private QuestInfo currentQuest;

    private void Awake()
    {
        questDic = GameObject.FindWithTag("DataController").GetComponent<QuestDictionary>();
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
        upgradeDic = GameObject.FindWithTag("DataController").GetComponent<UpgradeDictionary>();

        btn = gameObject.GetComponent<Button>();
        btnImg = gameObject.GetComponent<Image>();



        //btnImg = Resources.Load<Image>("questImg/quest_uncomplete");
        btn.enabled = false;

        btnColor_d.a = 0;
        btnColor_a.a = 255;
        btnColor_a.r = 255;
        btnColor_a.g = 255;
        btnColor_a.b = 255;

        LoadBtnInfo();

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
                StartCoroutine(Blink());
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

    public void OnClick()
    {
        currentQuest = questDic.FindQuest(DataController.GetInstance().GetQuestProcess());

        int checkItemIndex = currentQuest.termsItem;
        int checkItemCount = currentQuest.termsCount;
        int currentItemNum = 0;

        // 조건이 [골드/업그레이드/아이템] 인지 확인
        if (checkItemIndex == 9999)
        {
            currentItemNum = (int)DataController.GetInstance().GetGold();
        }
        else if (checkItemIndex > 50000)
        {
            currentItemNum = DataController.GetInstance().CheckUpgradeLevel(checkItemIndex);
        }
        else
        {
            currentItemNum = DataController.GetInstance().GetItemNum(checkItemIndex);
        }

        // 조건 아이템의 갯수 확인
        if (checkItemCount == currentItemNum)
        {
            if (currentQuest.reward == 9999)
            {
                DataController.GetInstance().AddGold((ulong)currentQuest.rewardCount);
            }
            else if (currentQuest.reward > 50000)
            {
                if (currentQuest.termsItem == 9999)
                {
                    DataController.GetInstance().SubGold((ulong)currentQuest.termsCount);
                }
                DataController.GetInstance().SetMaxUpgradeLevel(currentQuest.reward);
            }
            else
            {
                // 조건이 골드일 경우
                if (currentQuest.termsItem == 9999)
                {
                    DataController.GetInstance().SubGold((ulong)currentQuest.termsCount);
                }
                DataController.GetInstance().InsertItem(currentQuest.reward, currentQuest.rewardCount);
            }
            DataController.GetInstance().NextQuest();
        }

        // 퀘스트 정보 출력

        GameObject.Find("Name Displayer").GetComponent<Text>().text = btn.GetComponent<QuestInfo>().title;

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

            for (int i = 3001; i < 3007; i++)
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

        GameObject.Find("Content Displayer").GetComponent<Text>().text = btn.GetComponent<QuestInfo>().content;

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

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            btnImg.color = btnColor_d;
            yield return new WaitForSeconds(0.5f);
            btnImg.color = btnColor_a;
        }
    }
}