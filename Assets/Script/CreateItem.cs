using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateItem : MonoBehaviour {

    private int energy = 0;  // 에너지 
    private int energyPerClick = 20; // 클릭당 에너지 증가량
    private int energyMaxValue = 100; // 에너지 충전 최대량
    public GameObject item; // 아이템
    public Image img;
    public UnityEngine.UI.Button btn;

    static int itemId;

    private ItemDictionary itemDic;

    private void Awake()
    {
        itemId = 0;

        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
    }
    
    private static CreateItem instance;

    public static CreateItem GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CreateItem>();

            if (instance == null)
            {
                GameObject container = new GameObject("CreateItem");

                instance = container.AddComponent<CreateItem>();
            }
        }
        return instance;
    }

    void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();
        if (btn == null)
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        img.fillAmount = 0.0f; // 처음 버튼 게이지 0으로 -> 게이지 저장 가능 시 삭제해야함

    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetEnergy()
    {
        return energy;
    }

    public void AddEnergy() // 클릭 수 증가
    {
        energy += energyPerClick;
        img.fillAmount = (float)energy / energyMaxValue;
        
    }

    public void resetEnergy() // 클릭 수 초기화
    {
        btn.enabled = false;
        StartCoroutine(DecreaseEnergy());
    }

    IEnumerator DecreaseEnergy()
    {
        while (img.fillAmount != 0)
        {
            yield return new WaitForSeconds(0.05f);

            img.fillAmount -= 0.1f;
        }

        energy = 0;
        btn.enabled = true;

        yield return null;
    }

    public void OnClick() 
    {
        AddEnergy();
        newObject();
    }

    public void newObject() // 아이템 생성
    {

        if (energy >= energyMaxValue)
        {
            if (DataController.GetInstance().GetItemCount() >= DataController.GetInstance().GetItemLimit()) // 아이템 갯수 제한
            {
                Debug.Log("아이템 상자가 꽉 찼어요");
                return;
            }


            if (SwitchSunMoon.GetInstance().GetState() == 1) // sun일 때 나뭇가지 등 생성해야함
            {
                int num = (int)Random.Range(0.0f, 99.0f);
                if (num >= 95)
                {
                    int productID = (int)Random.Range(2007.0f, 2013.0f);

                    //Instantiate(stick, new Vector3(-213, -396, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false); // canvas 자식으로 상속해서 prifab생성
                    GameObject newItem = Instantiate(item, new Vector3(-758, -284, -4), Quaternion.identity);

                    // 현재 보유하고 있는 재료를 관리하는 Dictionary에 방금 생성한 item을 넣어준다.
                    itemDic.InsertItem(itemId, productID);

                    ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
                    ItemInfo findItemInfo = itemDic.findDic[productID];

                    itemInfo.id = itemId;
                    itemInfo.index = productID;
                    itemInfo.mtName = findItemInfo.mtName;
                    itemInfo.grade = findItemInfo.grade;
                    itemInfo.element = findItemInfo.element;
                    itemInfo.getRoot = findItemInfo.getRoot;
                    itemInfo.sellPrice = findItemInfo.sellPrice;
                    itemInfo.description = findItemInfo.description;
                    itemInfo.imagePath = findItemInfo.imagePath;

                    newItem.GetComponent<BoxCollider2D>().isTrigger = false;
                    newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);

                    itemId++;

                }
                else
                {
                    int productID = (int)Random.Range(2001.0f, 2007.0f);

                    //Instantiate(stick, new Vector3(-213, -396, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false); // canvas 자식으로 상속해서 prifab생성
                    GameObject newItem = Instantiate(item, new Vector3(-758, -284, -4), Quaternion.identity);

                    // 현재 보유하고 있는 재료를 관리하는 Dictionary에 방금 생성한 item을 넣어준다.
                    itemDic.InsertItem(itemId, productID);

                    ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
                    ItemInfo findItemInfo = itemDic.findDic[productID];

                    itemInfo.id = itemId;
                    itemInfo.index = productID;
                    itemInfo.mtName = findItemInfo.mtName;
                    itemInfo.grade = findItemInfo.grade;
                    itemInfo.element = findItemInfo.element;
                    itemInfo.getRoot = findItemInfo.getRoot;
                    itemInfo.sellPrice = findItemInfo.sellPrice;
                    itemInfo.description = findItemInfo.description;
                    itemInfo.imagePath = findItemInfo.imagePath;

                    newItem.GetComponent<BoxCollider2D>().isTrigger = false;
                    newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);

                    itemId++;
                }

                
            }
            else
            {
                int num = (int)Random.Range(0.0f, 99.0f);
                if (num >= 95)
                {
                    int productID = (int)Random.Range(1004.0f, 1007.0f);

                    //Instantiate(stick, new Vector3(-213, -396, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false); // canvas 자식으로 상속해서 prifab생성
                    GameObject newItem = Instantiate(item, new Vector3(-758, -284, -4), Quaternion.identity);

                    // 현재 보유하고 있는 재료를 관리하는 Dictionary에 방금 생성한 item을 넣어준다.
                    itemDic.InsertItem(itemId, productID);

                    ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
                    ItemInfo findItemInfo = itemDic.findDic[productID];

                    itemInfo.id = itemId;
                    itemInfo.index = productID;
                    itemInfo.mtName = findItemInfo.mtName;
                    itemInfo.grade = findItemInfo.grade;
                    itemInfo.element = findItemInfo.element;
                    itemInfo.getRoot = findItemInfo.getRoot;
                    itemInfo.sellPrice = findItemInfo.sellPrice;
                    itemInfo.description = findItemInfo.description;
                    itemInfo.imagePath = findItemInfo.imagePath;

                    newItem.GetComponent<BoxCollider2D>().isTrigger = false;
                    newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);

                    itemId++;
                }
                else
                {
                    int productID = (int)Random.Range(1001.0f, 1004.0f);

                    //Instantiate(stick, new Vector3(-213, -396, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false); // canvas 자식으로 상속해서 prifab생성
                    GameObject newItem = Instantiate(item, new Vector3(-758, -284, -4), Quaternion.identity);

                    // 현재 보유하고 있는 재료를 관리하는 Dictionary에 방금 생성한 item을 넣어준다.
                    itemDic.InsertItem(itemId, productID);

                    ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
                    ItemInfo findItemInfo = itemDic.findDic[productID];

                    itemInfo.id = itemId;
                    itemInfo.index = productID;
                    itemInfo.mtName = findItemInfo.mtName;
                    itemInfo.grade = findItemInfo.grade;
                    itemInfo.element = findItemInfo.element;
                    itemInfo.getRoot = findItemInfo.getRoot;
                    itemInfo.sellPrice = findItemInfo.sellPrice;
                    itemInfo.description = findItemInfo.description;
                    itemInfo.imagePath = findItemInfo.imagePath;

                    newItem.GetComponent<BoxCollider2D>().isTrigger = false;
                    newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);

                    itemId++;
                }


            }
            DataController.GetInstance().AddItemCount();
            resetEnergy();
            //img.fillAmount = 0.0f;
        }
        
    }

   
}
