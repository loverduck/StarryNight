using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
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

    // 현재 보유 골드량, 현재 보유 아이템 개수, 아이템 개수 제한, 클릭당 올라가는 게이지 양, 퀘스트 진행도(인덱스)
    private int m_gold, m_itemcount, m_itemlimit, m_energyPerClick, m_questProcess;
    private float m_leftTime;

    /// <summary>
    /// NOTE: 현재 내가 소지하고 있는 재료 Dictionary
    /// <para>-> key(int) : 게임 오브젝트를 구별하는 id</para>
    /// <para>-> value(int) : 재료 기준표 인덱스</para>
    /// </summary>
    public Dictionary<int, int> haveDic;

    // 게임 초기화될 때 
    void Awake()
    {
        DontDestroyOnLoad(this);

        // Key : Value로써 PlayerPrefs에 저장
        m_gold = PlayerPrefs.GetInt("Gold", 0);
        m_itemcount = PlayerPrefs.GetInt("ItemCount", 0);
        m_itemlimit = PlayerPrefs.GetInt("ItemLimit", 10);
        m_energyPerClick = PlayerPrefs.GetInt("EnergyPerClick", 20);
        m_questProcess = PlayerPrefs.GetInt("QuestProcess", 90102);
        m_leftTime = PlayerPrefs.GetFloat("LeftTime", 300.0f);

        haveDic = new Dictionary<int, int>();
    }

    void Update()
    {
        m_leftTime -= Time.deltaTime;
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

    /// <summary>
    /// 아이템을 추가하는 함수
    /// </summary>
    /// <param name="key">추가하는 아이템의 index</param>
    /// <param name="value">추가하는 아이템의 개수</param>
    public void InsertItem(int key)
    {
        int value = haveDic[key];
        haveDic[key] += value;
    }

    /// <summary>
    /// 아이템을 삭제하는 함수
    /// </summary>
    /// <param name="key">삭제할 아이템의 key값</param>
    public void DeleteItem(int key)
    {
        int value = haveDic[key];
        haveDic[key] -= value;
    }

    /// <summary>
    /// 현재 보유하고 있는 아이템을 보여주는 함수
    /// </summary>
    /// <param name="key">haveDic의 key값</param>
    /// <returns></returns>
    public bool CheckExistItem(int key)
    {
        return haveDic.ContainsKey(key);
    }

    /// <summary>
    /// 현재 보유하고 있는 아이템의 갯수를 보여주는 함수
    /// </summary>
    /// <param name="key">haveDic의 key값</param>
    /// <returns></returns>
    public int GetItemNum(int key)
    {
        if (CheckExistItem(key))
        {
            return haveDic[key];
        }

        return 0;
    }

    public int GetQuestProcess()
    {
        return m_questProcess;
    }

    public float GetLeftTime()
    {
        return m_leftTime;
    }

    public void SetLeftTime(float time)
    {
        m_leftTime = time;
    }
}