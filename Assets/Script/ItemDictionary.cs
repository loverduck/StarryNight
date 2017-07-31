using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Tuple<T1, T2>
{
    public readonly T1 m_item1;
    public readonly T2 m_item2;

    public Tuple(T1 item1, T2 item2)
    {
        m_item1 = item1;
        m_item2 = item2;
    }
}

public class ItemDictionary : MonoBehaviour
{
    enum FILEINFO
    {
        COMBINETABLE,
        ITEMTABLE,
        SETITEMTABLE
    }

    /// <summary>
    /// NOTE: 재료를 찾을 때 사용하는 Dictionary
    /// <para> -> key(int) : 재료 기준표 인덱스</para>
    /// <para> -> value(ItemInfo) : 재료 정보</para>
    /// </summary>
    public Dictionary<int, ItemInfo> findDic;

    /// <summary>
    /// NOTE: 현재 내가 소지하고 있는 재료 Dictionary
    /// <para>-> key(int) : 게임 오브젝트를 구별하는 id</para>
    /// <para>-> value(int) : 재료 기준표 인덱스</para>
    /// </summary>
    public Dictionary<int, int> haveDic;

    /// <summary>
    /// NOTE : 재료 조합식 Dictionary
    /// <para>-> key(int) : material1 인덱스</para>
    /// <para>-> value(int) : material1에 해당하는 조합식 list</para>
    /// </summary>
    public Dictionary<Tuple<int, int>, int> cbDic;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        // 두 Dictionary들을 초기화
        findDic = new Dictionary<int, ItemInfo>();
        haveDic = new Dictionary<int, int>();
        cbDic = new Dictionary<Tuple<int, int>, int>();

        ReadDataFile("dataTable/combineTable", FILEINFO.COMBINETABLE);
        ReadDataFile("dataTable/itemTable", FILEINFO.ITEMTABLE);

        SceneManager.LoadScene("Main");
    }

    private void ReadDataFile(string fileName, FILEINFO fileType)
    {
        TextAsset txtFile = (TextAsset)Resources.Load(fileName) as TextAsset;
        string txt = txtFile.text;
        string[] stringOperators = new string[] { "\r\n" };
        string[] lineList = txt.Split(stringOperators, StringSplitOptions.None);

        int lineListLen = lineList.Length;

        for (int i = 0; i < lineListLen; i++)
        {
            string[] wordList = lineList[i].Split(',');
            switch (fileType)
            {
                case FILEINFO.COMBINETABLE:
                    int material1 = Convert.ToInt32(wordList[0]);
                    int material2 = Convert.ToInt32(wordList[1]);
                    int result = Convert.ToInt32(wordList[2]);

                    if (material1 != material2)
                    {
                        cbDic[new Tuple<int, int>(material2, material1)] = result;
                    }

                    cbDic[new Tuple<int, int>(material1, material2)] = result;
                    
                    break;
                case FILEINFO.ITEMTABLE:
                    int index = Convert.ToInt32(wordList[0]);

                    findDic[index] = gameObject.AddComponent<ItemInfo>();

                    findDic[index].Init(0, index, wordList[1], Convert.ToInt32(wordList[2]), Convert.ToInt32(wordList[3]), Convert.ToInt32(wordList[4]), Convert.ToInt32(wordList[5]), wordList[6], "itemImg/item_" + index);
                    break;
                case FILEINFO.SETITEMTABLE:
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 아이템을 추가하는 함수
    /// </summary>
    /// <param name="key">추가하는 아이템의 key값</param>
    /// <param name="value">추가하는 아이템의 value값</param>
    public void InsertItem(int key, int value)
    {
        haveDic[key] = value;
    }

    /// <summary>
    /// 아이템을 삭제하는 함수
    /// </summary>
    /// <param name="key">삭제할 아이템의 key값</param>
    public void DeleteItem(int key)
    {
        haveDic.Remove(key);
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
    /// 기준표에서 아이템을 찾는 함수
    /// </summary>
    /// <param name="key">findDic의 key값</param>
    /// <returns>리턴값</returns>
    public ItemInfo FindItem(int key)
    {
        return findDic[key];
    }

    /// <summary>
    /// 조합표에서 검색하는 함수
    /// </summary>
    /// <param name="key1">재료1의 인덱스</param>
    /// <param name="key2">재료2의 인덱스</param>
    /// <returns>찾았다면 결과를, 아니라면 0을 반환</returns>
    public int FindCombine(int key1, int key2)
    {
        Tuple<int, int> tuple = new Tuple<int, int>(key1, key2);

        if (cbDic.ContainsKey(tuple))
        {
            return cbDic[tuple];
        }

        return 0;
    }
}