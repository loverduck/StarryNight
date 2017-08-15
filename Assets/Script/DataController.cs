using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataController : MonoBehaviour
{
    // 현재 보유 골드량, 현재 보유 아이템 개수, 아이템 개수 제한, 클릭당 올라가는 게이지 양
    private ulong m_gold;
    private int m_itemcount, m_itemlimit, m_energyPerClick;
    
    private int debugInt;

    /// <summary>
    /// NOTE: 현재 내가 소지하고 있는 재료 Dictionary
    /// <para>-> key(int) : 게임 오브젝트를 구별하는 id</para>
    /// <para>-> value(HaveDicInfo) : 재료 기준표 정보</para>
    /// </summary>
    public Dictionary<int, int> haveDic;

    /// <summary>
    /// NOTE: 열린 도감을 저장하는 Dictionary
    /// <para>-> key(int) : 도감이 열린 재료 index</para>
    /// </summary>
    public List<int> itemOpenList;

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

    // 게임 초기화될 때 
    void Awake()
    {
        DontDestroyOnLoad(this);

        // Key : Value로써 PlayerPrefs에 저장
        m_gold = Convert.ToUInt64(PlayerPrefs.GetString("Gold", "0"));
        m_itemcount = PlayerPrefs.GetInt("ItemCount", 0);
        m_itemlimit = PlayerPrefs.GetInt("ItemLimit", 10);
        m_energyPerClick = PlayerPrefs.GetInt("EnergyPerClick", 20);

        haveDic = LoadGameData("/FileData/haveDic.txt") as Dictionary<int, int>;

        if (haveDic != null)
        {
            Debug.Log("haveDic Length is not 0");
        }
        else
        {
            Debug.Log("haveDic Length is 0");

            haveDic = new Dictionary<int, int>();
        }

        itemOpenList = LoadGameData("/FileData/itemOpenList.txt") as List<int>;

        if (itemOpenList != null)
        {
            Debug.Log("itemOpenList Length is not 0");
        }
        else
        {
            Debug.Log("itemOpenList Length is 0");

            itemOpenList = new List<int>();
        }
    }

    // 게임 데이터를 불러오는 함수
    private object LoadGameData(string dataPath)
    {
        string filePath = Application.dataPath + dataPath;

        if (File.Exists(filePath))
        {
            return DataDeserialize(File.ReadAllBytes(filePath));
        }

        return null;
    }

    // 데이터를 역직렬화하는 함수
    private object DataDeserialize(byte[] buffer)
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream mStream = new MemoryStream();

        mStream.Write(buffer, 0, buffer.Length);
        mStream.Position = 0;

        return binFormatter.Deserialize(mStream);
    }
    
    // 게임 데이터를 저장하는 함수
    private void SaveGameData(object data, string dataPath)
    {
        byte[] stream = DataSerialize(data);        
        File.WriteAllBytes(Application.dataPath + dataPath, stream);
    }

    // 데이터를 직렬화하는 함수
    private byte[] DataSerialize(object data)
    {
        BinaryFormatter binFormmater = new BinaryFormatter();
        MemoryStream mStream = new MemoryStream();

        binFormmater.Serialize(mStream, data);

        return mStream.ToArray();
    }

    /// <summary>
    /// gold 설정 함수
    /// </summary>
    /// <param name="newGold">새로 설정할 gold</param>
    public void SetGold(ulong newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetString("Gold", m_gold.ToString());
    }

    /// <summary>
    /// gold 더하는 함수
    /// </summary>
    /// <param name="newGold"> +할 gold</param>
    public void AddGold(ulong newGold)
    {
        SetGold(m_gold + newGold);
    }

    /// <summary>
    /// gold 빼는 함수
    /// </summary>
    /// <param name="newGold">-할 gold</param>
    public void SubGold(ulong newGold)
    {
        SetGold(m_gold - newGold);
    }

    // gold 가져오는 함수
    public ulong GetGold()
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
        if (!CheckExistItem(key))
        {
            itemOpenList.Add(key);
            //UM_Storage.Save("itemOpenList", DataSerialize(itemOpenList));
            SaveGameData(itemOpenList, "/FileData/itemOpenList.txt");

            Debug.Log("itemOpenList - DataSerialize");

            haveDic.Add(key, 1);
        }
        else
        {
            haveDic[key]++;
        }

        //UM_Storage.Save("haveDic", DataSerialize(haveDic));
        SaveGameData(haveDic, "/FileData/haveDic.txt");

        Debug.Log("InsertItem - haveDic DataSerialize");
    }

    /// <summary>
    /// 아이템을 삭제하는 함수
    /// </summary>
    /// <param name="key">삭제할 아이템의 key값</param>
    public void DeleteItem(int key)
    {
        if (haveDic[key] < 0)
        {
            // 디버깅용 코드
            Debug.LogError("Delete Item is under 0");
        }

        haveDic[key]--;

        UM_Storage.Save("haveDic", DataSerialize(haveDic));
        SaveGameData(haveDic, "/FileData/haveDic.txt");
        
        Debug.Log("DeleteItem() - haveDic DataSerialize");
    }

    /// <summary>
    /// 현재 이 아이템을 보유하고 있는지 확인하는 함수
    /// </summary>
    /// <param name="key">haveDic의 key값</param>
    /// <returns></returns>
    public bool CheckExistItem(int key)
    {
        return haveDic.ContainsKey(key);
    }

    public int GetItemNum(int key)
    {
        if (CheckExistItem(key))
        {
            return haveDic[key];
        }

        return 0;
    }
}