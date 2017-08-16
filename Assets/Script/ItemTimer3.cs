using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemTimer3 : MonoBehaviour
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

    public Button combineButton;

    private void Awake()
    {
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
    }

    private void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();

        if (btn == null)
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataController.GetInstance().GetLeftTimer3() > 0)
        {
            btn.enabled = false;
            sec = (int)DataController.GetInstance().GetLeftTimer3() % 60;
            sec_10 = (int)sec / 10;
            sec_1 = (int)sec % 10;
            min = (int)DataController.GetInstance().GetLeftTimer3() / 60;
            timeDisplayer.text = min + ":" + sec_10 + sec_1;

            if (DataController.GetInstance().GetLeftTimer3() < 0)
            {
                DataController.GetInstance().SetLeftTimer3(0);
                if (btn)
                {
                    btn.enabled = true;
                }

            }
            float ratio = 1.0f - (DataController.GetInstance().GetLeftTimer3() / cooltime);
            if (img)
                img.fillAmount = ratio;
        }
        else
        {
            timeDisplayer.text = "0:00";
            img.fillAmount = 1.0f;
            DataController.GetInstance().SetLeftTimer3(0);
            if (btn)
            {
                btn.enabled = true;
            }
        }
    }

    public bool CheckCooltime()
    {
        if (DataController.GetInstance().GetLeftTimer3() > 0)
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
            AudioManager.GetInstance().ItemSound();

            DataController.GetInstance().SetLeftTimer3(cooltime);
            btn.enabled = false;

            DataController.GetInstance().AddItemCount();
        }
    }

    private void CreateSetItem(int productID)
    {
        GameObject setItem = Instantiate(prefab, new Vector3(-480, 772, -3), Quaternion.identity);

        DataController.GetInstance().InsertItem(productID, 1);

        SetItemInfo setItemInfo = ItemDictionary.GetInstance().CheckSetItemCombine(productID);

        if (setItemInfo.result != 0)
        {
            combineButton.gameObject.SetActive(true);
            combineButton.onClick.AddListener(() => OnClick(setItemInfo));
        }

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

    void OnClick(SetItemInfo setItemInfo)
    {
        DataController dataController = DataController.GetInstance();

        dataController.DeleteItem(setItemInfo.index1);
        dataController.DeleteItem(setItemInfo.index2);
        dataController.DeleteItem(setItemInfo.index3);
        dataController.DeleteItem(setItemInfo.index4);

        dataController.InsertItem(setItemInfo.result, 1);

        SceneManager.LoadScene("Main");
    }
}