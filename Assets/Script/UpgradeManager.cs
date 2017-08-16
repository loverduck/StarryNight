using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private UpgradeDictionary upgradeDic;

    private GameObject invenUnlock;
    private GameObject perClickUnlock;

    private Text invenUp_Displayer;
    private Text invenUpCost_Displayer;
    private Text energyPerClickUp_Displayer;
    private Text energyPerClickUpCost_Displayer;
    private Button invenUpBtn;
    private Button energyPerClickUpBtn;

    private int currentInvenLv;
    private int currentPerClickLv;

    private void Awake()
    {
        upgradeDic = GameObject.FindWithTag("DataController").GetComponent<UpgradeDictionary>();

        invenUnlock = GameObject.Find("Inven Unlock Panel");
        perClickUnlock = GameObject.Find("PerClick Unlock Panel");

        invenUp_Displayer = GameObject.Find("Inven Upgrade Displayer").GetComponent<Text>();
        invenUpCost_Displayer = GameObject.Find("Inven Upgrade Cost Displayer").GetComponent<Text>();
        energyPerClickUp_Displayer = GameObject.Find("EnergyPerClick Upgrade Displayer").GetComponent<Text>();
        energyPerClickUpCost_Displayer = GameObject.Find("EnergyPerClick Upgrade Cost Displayer").GetComponent<Text>();
        invenUpBtn = GameObject.Find("Inven Upgrade Button").GetComponent<Button>();
        energyPerClickUpBtn = GameObject.Find("EnergyPerClick Upgrade Button").GetComponent<Button>();
    }

    void Update()
    {
        currentInvenLv = DataController.GetInstance().GetInvenLv();
        currentPerClickLv = DataController.GetInstance().GetEnergyPerClickLv();

        // 인벤 업그레이드 버튼 설정
        if (currentInvenLv >= DataController.GetInstance().GetMaxInvenLv())
        {
            invenUpBtn.enabled = false;
            if (currentInvenLv == 20)
            {
                invenUnlock.SetActive(false);
            }
            else
            {
                invenUnlock.SetActive(true);
            }
            
        }
        else
        {
            if (DataController.GetInstance().GetGold() < (ulong)upgradeDic.FindUpgrade(50001).cost[currentInvenLv])
            {
                invenUpBtn.enabled = false;
                invenUnlock.SetActive(true);
            }
            else
            {
                invenUpBtn.enabled = true;
                invenUnlock.SetActive(false);
            }
        }

        // 클릭 당 게이지 업그레이드 버튼 설정
        if (currentPerClickLv >= DataController.GetInstance().GetMaxPerClickLv())
        {
            energyPerClickUpBtn.enabled = false;
            if (currentPerClickLv == 20)
            {
                perClickUnlock.SetActive(false);
            }
            else
            {
                perClickUnlock.SetActive(true);
            }
        }
        else
        {
            if (DataController.GetInstance().GetGold() < (ulong)upgradeDic.FindUpgrade(50002).cost[currentPerClickLv])
            {
                energyPerClickUpBtn.enabled = false;
                perClickUnlock.SetActive(true);
            }
            else
            {
                energyPerClickUpBtn.enabled = true;
                perClickUnlock.SetActive(false);
            }
        }

        // value[current값] -> 꼭!
        if (currentInvenLv == 20)
        {
            invenUp_Displayer.text = "MAX";
            invenUpCost_Displayer.text = "MAX";
        }
        else
        {
            int nextInvenValue = 10 + upgradeDic.findUpDic[50001].value[currentInvenLv];
            invenUp_Displayer.text = "인벤토리 +" + DataController.GetInstance().GetItemLimit() + " -> +" + nextInvenValue;
            invenUpCost_Displayer.text = upgradeDic.FindUpgrade(50001).cost[currentInvenLv] + "원";
        }

        if (currentPerClickLv == 20)
        {
            energyPerClickUp_Displayer.text = "MAX";
            energyPerClickUpCost_Displayer.text = "MAX";
        }
        else
        {
            int nextClickValue = 2 + upgradeDic.findUpDic[50002].value[currentPerClickLv];
            energyPerClickUp_Displayer.text = "클릭당 게이지 +" + DataController.GetInstance().GetEnergyPerClick() + " -> +" + nextClickValue;
            energyPerClickUpCost_Displayer.text = upgradeDic.FindUpgrade(50002).cost[currentPerClickLv] + "원";
        }

    }

    public void InvenUpgrade()
    {
        // 골드 빼고
        DataController.GetInstance().SubGold((ulong)upgradeDic.FindUpgrade(50001).cost[currentInvenLv]);

        // 공간 늘려주고
        DataController.GetInstance().UpgradeInvenLv();
    }

    public void EnergyPerClickUpgrade()
    {
        // 골드 빼고
        DataController.GetInstance().SubGold((ulong)upgradeDic.FindUpgrade(50002).cost[currentPerClickLv]);

        // 클릭 당 게이지 증가시켜주고
        DataController.GetInstance().UpgradeEnergyPerClickLv();
    }
}
