using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    enum FILEINFO
    {
        COMBINETABLE,
        ITEMTABLE,
        SETITEMTABLE
    }

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

    /// <summary>
    /// NOTE: 재료를 찾을 때 사용하는 Dictionary
    /// <para> -> key(int) : 재료 기준표 인덱스</para>
    /// <para> -> value(ItemInfo) : 재료 정보</para>
    /// </summary>
    public Dictionary<int, ItemInfo> findDic;

    /// <summary>
    /// NOTE : 재료 조합식 Dictionary
    /// <para>-> key(int) : material1 인덱스</para>
    /// <para>-> value(int) : material1에 해당하는 조합식 list</para>
    /// </summary>
    public Dictionary<Tuple<int, int>, List<int>> cbDic;

    public int starNum { get; private set; }
    public int materialNum { get; private set; }
    public int combineNum { get; private set; }
    public int setNum { get; private set; }

    public static ItemDictionary instance;

    public static ItemDictionary GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ItemDictionary>();

            if (instance == null)
            {
                GameObject container = new GameObject("ItemDictionary");
                instance = container.AddComponent<ItemDictionary>();
            }
        }

        return instance;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        // 두 Dictionary들을 초기화
        findDic = new Dictionary<int, ItemInfo>();
        cbDic = new Dictionary<Tuple<int, int>, List<int>>();

        ReadDataFile("dataTable/combineTable", FILEINFO.COMBINETABLE);
        ReadDataFile("dataTable/itemTable", FILEINFO.ITEMTABLE);

        DataController.GetInstance().SetloadingFinish(true);

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
                    int resultNum = Convert.ToInt32(wordList[2]);

                    Tuple<int, int> tuple;

                    if (material1 != material2)
                    {
                        tuple = new Tuple<int, int>(material2, material1);

                        cbDic[tuple] = new List<int>();

                        for (int j = 0; j < resultNum; j++)
                        {
                            cbDic[tuple].Add(Convert.ToInt32(wordList[3 + j]));
                        }
                    }

                    tuple = new Tuple<int, int>(material1, material2);

                    cbDic[tuple] = new List<int>();

                    for (int j = 0; j < resultNum; j++)
                    {
                        cbDic[tuple].Add(Convert.ToInt32(wordList[3 + j]));
                    }

                    break;
                case FILEINFO.ITEMTABLE:
                    int index = Convert.ToInt32(wordList[0]);
                    int sellPrice = Convert.ToInt32(wordList[5]);
                    string group = wordList[2];

                    switch (group)
                    {
                        case "별":
                            starNum++;
                            break;
                        case "재료":
                            materialNum++;
                            break;
                        case "아이템":
                            combineNum++;
                            break;
                        case "서적":
                            setNum++;
                            break;
                        default:
                            break;
                    }

                    findDic[index] = gameObject.AddComponent<ItemInfo>();
                    findDic[index].Init(index, wordList[1], group, wordList[3], sellPrice, wordList[4], "itemImg/item_" + index);

                    break;
                case FILEINFO.SETITEMTABLE:
                    break;
                default:
                    break;
            }
        }
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
    public List<int> FindCombine(int key1, int key2)
    {
        Tuple<int, int> tuple = new Tuple<int, int>(key1, key2);

        if (cbDic.ContainsKey(tuple))
        {
            return cbDic[tuple];
        }

        return null;
    }
}