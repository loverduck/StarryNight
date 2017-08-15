using UnityEngine;
using UnityEngine.UI;

public class ItemListManager : MonoBehaviour
{
    int starIdxStart, materialIdxStart, combineIdxStart, setIdxStart;
    int starIdxMax, materialIdxMax, combineIdxMax, setIdxMax;

    ItemDictionary itemDic;

    public Button btn;
    public GameObject itemInfoWindow;

    Transform starContentPanel, materialContentPanel, combineContentPanel, setContentPanel;

    private static ItemListManager instance;

    public static ItemListManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<ItemListManager>();

            if (instance == null)
            {
                GameObject container = new GameObject("ItemListManager");
                instance = container.AddComponent<ItemListManager>();
            }
        }

        return instance;
    }

    private void Awake()
    {
        itemDic = DataController.GetInstance().GetComponent<ItemDictionary>();

        starIdxStart = 1000;
        materialIdxStart = 2000;
        combineIdxStart = 3000;
        setIdxStart = 4000;

        starIdxMax = starIdxStart + itemDic.starNum;
        materialIdxMax = materialIdxStart + itemDic.materialNum;
        combineIdxMax = combineIdxStart + itemDic.combineNum;
        setIdxMax = setIdxStart + itemDic.setNum;

        starContentPanel = GameObject.Find("StarContentPanel").transform;
        materialContentPanel = GameObject.Find("MaterialContentPanel").transform;
        combineContentPanel = GameObject.Find("CombineContentPanel").transform;
        setContentPanel = GameObject.Find("SetContentPanel").transform;
    }

    private void Start()
    {
        for (int idx = starIdxStart + 1; idx <= starIdxMax; idx++)
        {
            AddItemButton(idx, starContentPanel);
        }

        for (int idx = materialIdxStart + 1; idx <= materialIdxMax; idx++)
        {
            AddItemButton(idx, materialContentPanel);
        }

        for (int idx = combineIdxStart + 1; idx <= combineIdxMax; idx++)
        {
            AddItemButton(idx, combineContentPanel);
        }

        for (int idx = setIdxStart + 1; idx <= setIdxMax; idx++)
        {
            AddItemButton(idx, setContentPanel);
        }
    }

    void AddItemButton(int idx, Transform tf)
    {
        Button itemBtn = Instantiate(btn);

        ItemInfo itemInfo = itemBtn.GetComponent<ItemInfo>();
        ItemInfo findItemInfo = itemDic.findDic[idx];

        itemInfo.index = idx;
        itemInfo.mtName = findItemInfo.mtName;
        itemInfo.group = findItemInfo.group;
        itemInfo.grade = findItemInfo.grade;
        itemInfo.sellPrice = findItemInfo.sellPrice;
        itemInfo.description = findItemInfo.description;
        itemInfo.imagePath = findItemInfo.imagePath;

        itemBtn.transform.SetParent(tf);
        itemBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(itemInfo.imagePath);

        itemBtn.onClick.AddListener(() => ShowWindow(itemInfo));
    }

    public void ShowWindow(ItemInfo itemInfo)
    {
        itemInfoWindow.gameObject.SetActive(true);

        ItemInfoWindow infoWindow = itemInfoWindow.GetComponent<ItemInfoWindow>();

        infoWindow.itemImg.sprite = Resources.Load<Sprite>(itemInfo.imagePath);
        infoWindow.itemName.text = itemInfo.mtName;
        infoWindow.itemSort.text = itemInfo.group;
        infoWindow.itemGrade.text = itemInfo.grade;
        infoWindow.itemCost.text = itemInfo.sellPrice.ToString();
        infoWindow.itemText.text = itemInfo.description;
    }
}