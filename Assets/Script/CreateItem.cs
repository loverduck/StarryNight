using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreateItem : MonoBehaviour
{
    private int energy = 0;  // 에너지 
    private int energyPerClick; // 클릭당 에너지 증가량
    private int energyMaxValue = 100; // 에너지 충전 최대량
    public GameObject item; // 아이템
    public Image img_earthback;
    public Button btn;

    private ItemDictionary itemDic;

    private void Awake()
    {
        energyPerClick = DataController.GetInstance().GetEnergyPerClick();
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
        if (img_earthback == null)
            img_earthback = GameObject.Find("EarthBack").GetComponent<Image>();

        if (btn == null)
            btn = gameObject.GetComponent<Button>();

        energy = DataController.GetInstance().GetEnergy();

        img_earthback.fillAmount = (float)energy / energyMaxValue;
    }


    public void AddEnergy() // 클릭 수 증가
    {
        energy += energyPerClick;
        img_earthback.fillAmount = (float)energy / energyMaxValue;
        DataController.GetInstance().SetEnergy(energy);
    }

    public void ResetEnergy() // 클릭 수 초기화
    {
        btn.enabled = false;
        StartCoroutine(DecreaseEnergy());
    }
    // 에너지 감소
    IEnumerator DecreaseEnergy()
    {
        while (img_earthback.fillAmount != 0)
        {
            yield return new WaitForSeconds(0.05f);

            img_earthback.fillAmount -= 0.1f;
        }

        energy = 0;
        DataController.GetInstance().SetEnergy(energy);
        btn.enabled = true;

        yield return null;
    }

    // item1maker 클릭 시
    public void OnClick()
    {
        AddEnergy();
        NewObject();
    }

    // 아이템 생성
    private void NewObject()
    {
        if (energy >= energyMaxValue)
        {
            if (DataController.GetInstance().GetItemCount() >= DataController.GetInstance().GetItemLimit()) // 아이템 갯수 제한
            {
                Debug.Log("아이템 상자가 꽉 찼어요~");

                return;
            }

            if (SwitchSunMoon.GetInstance().GetState() == 1) // sun일 때 나뭇가지 등 생성해야함
            {
                if (Random.Range(0, 100) >= 95)
                {
                    GenerateItem(Random.Range(2007, 2013));
                }
                else
                {
                    GenerateItem(Random.Range(2001, 2007));
                }
            }
            else
            {
                if (Random.Range(0, 100) >= 95)
                {
                    GenerateItem(Random.Range(1004, 1007));
                }
                else
                {
                    GenerateItem(Random.Range(1001, 1004));
                }
            }

            DataController.GetInstance().AddItemCount();
            ResetEnergy();

            //img.fillAmount = 0.0f;
        }
    }

    private void GenerateItem(int productID)
    {
        //Instantiate(stick, new Vector3(-213, -396, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false); // canvas 자식으로 상속해서 prifab생성
        GameObject newItem = Instantiate(item, new Vector3(-758, -284, -3), Quaternion.identity);

        // 현재 보유하고 있는 재료를 관리하는 Dictionary에 방금 생성한 item을 넣어준다.
        DataController.GetInstance().InsertItem(productID);

        ItemInfo itemInfo = newItem.GetComponent<ItemInfo>();
        ItemInfo findItemInfo = itemDic.findDic[productID];

        itemInfo.index = productID;
        itemInfo.mtName = findItemInfo.mtName;
        itemInfo.group = findItemInfo.group;
        itemInfo.grade = findItemInfo.grade;
        itemInfo.sellPrice = findItemInfo.sellPrice;
        itemInfo.description = findItemInfo.description;
        itemInfo.imagePath = findItemInfo.imagePath;

        newItem.GetComponent<BoxCollider2D>().isTrigger = false;
        newItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[productID].imagePath);
    }
}