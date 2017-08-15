using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimer2 : MonoBehaviour
{
    public GameObject prefab;
    public Text timeDisplayer;
    public Image img;
    public Button btn;
    float cooltime = 300.0f;
    public bool disableOnStart = false;
    private int sec;
    private int sec_1;
    private int sec_10;
    private int min;
    private ItemDictionary itemDic;

    private void Awake()
    {
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
    }

    private void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();
        if (btn == null)
            btn = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataController.GetInstance().GetLeftTimer2() > 0)
        {
            btn.enabled = false;
            sec = (int)DataController.GetInstance().GetLeftTimer2() % 60;
            sec_10 = (int)sec / 10;
            sec_1 = (int)sec % 10;
            min = (int)DataController.GetInstance().GetLeftTimer2() / 60;
            timeDisplayer.text = "0" + min + ":" + sec_10 + sec_1;

            if (DataController.GetInstance().GetLeftTimer2() < 0)
            {
                DataController.GetInstance().SetLeftTimer2(0);
                if (btn)
                {
                    btn.enabled = true;
                }

            }
            float ratio = 1.0f - (DataController.GetInstance().GetLeftTimer2() / cooltime);
            if (img)
                img.fillAmount = ratio;
        }
        else
        {
            img.fillAmount = 1.0f;
            DataController.GetInstance().SetLeftTimer2(0);
            if (btn)
            {
                btn.enabled = true;
            }
        }
    }

    public bool CheckCooltime()
    {
        if (DataController.GetInstance().GetLeftTimer2() > 0)
            return false;
        else
            return true;
    }

    public void ResetCooltime()
    {
        if (btn)
        {
            if (DataController.GetInstance().GetItemCount() >= DataController.GetInstance().GetItemLimit()) // 아이템 갯수 제한
            {
                Debug.Log("아이템 상자가 꽉 찼어요");
                return;
            }

            int id = Random.Range(4001, 4059);

            while (id % 5 == 0)
            {
                id = Random.Range(4001, 4059);
            }

            CreateSetItem(id);

            DataController.GetInstance().SetLeftTimer2(cooltime);
            btn.enabled = false;

            DataController.GetInstance().AddItemCount();
        }
    }

    private void CreateSetItem(int productID)
    {
        GameObject setItem = Instantiate(prefab, new Vector3(-600, 772, -4), Quaternion.identity);

        DataController.GetInstance().InsertItem(productID, 1);

        ItemInfo itemInfo = setItem.GetComponent<ItemInfo>();
        ItemInfo findItemInfo = itemDic.findDic[productID];

        itemInfo.index = productID;
        itemInfo.mtName = findItemInfo.mtName;
        itemInfo.group = findItemInfo.group;
        itemInfo.grade = findItemInfo.grade;
        itemInfo.sellPrice = findItemInfo.sellPrice;
        itemInfo.description = findItemInfo.description;
        itemInfo.imagePath = findItemInfo.imagePath;

        setItem.GetComponent<BoxCollider2D>().isTrigger = false;
        setItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);
    }
}