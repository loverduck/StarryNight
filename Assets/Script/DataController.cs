using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

    private static DataController instance;

    public static DataController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DataController>();

            if (instance == null)
            {
                GameObject container = new GameObject("DataController");

                instance = container.AddComponent<DataController>();
            }
        }
        return instance;
    }

    private int m_gold;
    private int m_itemcount = 0;
    private int m_itemlimit;
    private int m_energyPerClick;

    // 게임 초기화될 때 
    void Awake()
    {
        // Key : Value로써 PlayerPrefs에 저장
        m_gold = PlayerPrefs.GetInt("Gold");

        // 1은 기본 지정 값
        SetEnergyPerClick(20);
        //m_energyPerClick = PlayerPrefs.GetInt("EnergyPerClick", 20);

        m_itemlimit = PlayerPrefs.GetInt("ItemLimit", 10);

    }


    /// <summary>
    /// gold 설정 함수
    /// </summary>
    /// <param name="newGold">새로 설정할 gold</param>
    public void SetGold(int newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetInt("Gold", m_gold);
    }

    /// <summary>
    /// gold 더하는 함수
    /// </summary>
    /// <param name="newGold"> +할 gold</param>
    public void AddGold(int newGold)
    {
        SetGold(m_gold + newGold);
    }

    /// <summary>
    /// gold 빼는 함수
    /// </summary>
    /// <param name="newGold">-할 gold</param>
    public void SubGold(int newGold)
    {
        SetGold(m_gold - newGold);
    }

    // gold 가져오는 함수
    public int GetGold()
    {
        return m_gold;
    }

    // item 개수 세는 함수
    public int GetItemCount()
    {
        return m_itemcount;
    }

    // item 개수 더하는 함수
    public void AddItemCount()
    {
        m_itemcount += 1;
    }

    public void SubItemCount()
    {
        m_itemcount -= 1;
    }

    // item 최대 개수
    public int GetItemLimit()
    {
        return m_itemlimit;
    }

    /// <summary>
    ///  item 최대 개수 설정 함수
    /// </summary>
    /// <param name="newItemLimit">새로운 item 최대 개수</param>
    public void SetItemLimit(int newItemLimit)
    {
        m_itemlimit = newItemLimit;
        PlayerPrefs.SetInt("ItemLimit", m_itemlimit);
    }

    /// <summary>
    /// EnergyPerClick을 얻는 함수
    /// </summary>
    /// <returns></returns>
    public int GetEnergyPerClick()
    {
        return m_energyPerClick;
    }

    /// <summary>
    /// EnergyPerClick을 설정하는 함수
    /// </summary>
    /// <param name="newEnergyPerClick">설정할 EnergyPerClick</param>
    public void SetEnergyPerClick(int newEnergyPerClick)
    {
        m_energyPerClick = newEnergyPerClick;
        PlayerPrefs.SetInt("EnergyPerClick", m_energyPerClick);
    }

    /// <summary>
    /// EnergyPerClick을 더하는 함수
    /// </summary>
    /// <param name="newEnergyPerClick">+할 EnergyPerClick</param>
    public void AddEnergyPerClick(int newEnergyPerClick)
    {
        SetEnergyPerClick(m_energyPerClick + newEnergyPerClick);
    }

    
}
