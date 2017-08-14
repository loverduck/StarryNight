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
    private int m_gold, m_itemcount, m_questProcess;
    private float m_leftTimer1, m_leftTimer2, m_leftTimer3;

    // 현재 인벤토리 레벨, 클릭 게이지 레벨
    private int invenLv, energyPerClickLv;

    // 최대 업그레이드 가능한 - 인벤토리 레벨, 클릭 게이지 레벨
    private int invenMaxLv, energyPerClickMaxLv;

    /// <summary>
    /// NOTE: 현재 내가 소지하고 있는 재료 Dictionary
    /// <para>-> key(int) : 게임 오브젝트를 구별하는 id</para>
    /// <para>-> value(int) : 재료 기준표 인덱스</para>
    /// </summary>
    public Dictionary<int, int> haveDic;

    private UpgradeDictionary upgradeDic;

    // 게임 초기화될 때 
    void Awake()
    {
        DontDestroyOnLoad(this);

        upgradeDic = GameObject.FindWithTag("DataController").GetComponent<UpgradeDictionary>();

        // Key : Value로써 PlayerPrefs에 저장
        m_gold = PlayerPrefs.GetInt("Gold", 0);
        m_itemcount = PlayerPrefs.GetInt("ItemCount", 0);
        m_questProcess = PlayerPrefs.GetInt("QuestProcess", 90117);
        m_leftTimer1 = PlayerPrefs.GetFloat("LeftTimer1", 300.0f);
        m_leftTimer2 = PlayerPrefs.GetFloat("LeftTimer2", 300.0f);
        m_leftTimer3 = PlayerPrefs.GetFloat("LeftTimer3", 300.0f);

        invenLv = PlayerPrefs.GetInt("InvenLevel", 0);
        energyPerClickLv = PlayerPrefs.GetInt("EnergyPerClickLevel", 0);

        invenMaxLv = PlayerPrefs.GetInt("InvenMaxLevel", 20);
        energyPerClickMaxLv = PlayerPrefs.GetInt("EnergyPerClickMaxLevel", 20);

        haveDic = new Dictionary<int, int>();
    }

    void Update()
    {
        m_leftTimer1 -= Time.deltaTime;
        m_leftTimer2 -= Time.deltaTime;
        m_leftTimer3 -= Time.deltaTime;
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
        PlayerPrefs.GetInt("ItemCount", m_itemcount);
    }

    public void SubItemCount()
    {
        m_itemcount -= 1;
        PlayerPrefs.GetInt("ItemCount", m_itemcount);
    }


    // item 최대 개수 가져오기 
    public int GetItemLimit()
    {
        if (invenLv == 0)
        {
            return 10;
        }
        else
        {
            return 10 + upgradeDic.FindUpgrade(50001).value[invenLv - 1];
        }
    }

    public int GetInvenLv()
    {
        return invenLv;
    }

    public void UpgradeInvenLv()
    {
        invenLv += 1;
        PlayerPrefs.GetInt("InvenLevel", invenLv);
    }



    /// <summary>
    /// EnergyPerClick을 얻는 함수
    /// </summary>
    /// <returns></returns>
    public int GetEnergyPerClick()
    {
        return 2 + 2 * energyPerClickLv;
    }

    public int GetEnergyPerClickLv()
    {
        return energyPerClickLv;
    }

    public void UpgradeEnergyPerClickLv()
    {
        energyPerClickLv += 1;
        PlayerPrefs.GetInt("EnergyPerClickLevel", energyPerClickLv);
    }


    /// <summary>
    /// 아이템을 추가하는 함수
    /// </summary>
    /// <param name="key">추가하는 아이템의 index</param>
    public void InsertItem(int key)
    {
        haveDic[key] += 1;
    }

    /// <summary>
    /// 아이템을 num만큼 추가하는 함수
    /// </summary>
    /// <param name="key">추가하는 아이템의 index</param>
    /// <param name="num">추가하는 아이템의 개수</param>
    public void AddItem(int key, int num)
    {
        haveDic[key] += num;
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

    /// <summary>
    /// 다음 퀘스트로 넘어가기
    /// </summary>
    public void NextQuest()
    {
        m_questProcess += 1;
        PlayerPrefs.GetInt("QuestProcess", m_questProcess);
    }

    public float GetLeftTimer1()
    {
        return m_leftTimer1;
    }

    public void SetLeftTimer1(float time)
    {
        m_leftTimer1 = time;
    }

    public float GetLeftTimer2()
    {
        return m_leftTimer2;
    }

    public void SetLeftTimer2(float time)
    {
        m_leftTimer2 = time;
    }

    public float GetLeftTimer3()
    {
        return m_leftTimer3;
    }

    public void SetLeftTimer3(float time)
    {
        m_leftTimer3 = time;
    }

    public int GetMaxInvenLv()
    {
        return invenMaxLv;
    }

    public void SetMaxInvenLv()
    {
        invenMaxLv += 1;
        PlayerPrefs.GetInt("InvenMaxLevel", invenMaxLv);
    }

    public int GetMaxPerClickLv()
    {
        return energyPerClickMaxLv;
    }

    public void SetMaxPerClickLv()
    {
        energyPerClickMaxLv += 1;
        PlayerPrefs.GetInt("EnergyPerClickMaxLevel", energyPerClickMaxLv);
    }

    // 업그레이드 인덱스로 현재 업그레이드 레벨 찾기
    public int CheckUpgradeLevel(int index)
    {
        if (index == 50001)
        {
            return invenLv;
        }
        else if (index == 50002)
        {
            return energyPerClickLv;
        }
        else
        {
            return 0;
        }
    }

    // 업그레이드 인덱스로 최대 업그레이드 레벨 올리기
    public void SetMaxUpgradeLevel(int index)
    {
        if (index == 50001)
        {
            SetMaxInvenLv();
        }
        else if (index == 50002)
        {
            SetMaxPerClickLv();
        }
    }
}