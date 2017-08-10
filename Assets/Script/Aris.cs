using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aris : MonoBehaviour {

    public Text nameDisplayer;
    public Text progressDisplayer;
    public Text contentDisplayer;
    public Text rewardDisplayer;

    public Button btn_90101;
    public Button btn_90102;
    public Button btn_90103;
    public Button btn_90104;

    private QuestDictionary questDic;
    private ItemDictionary itemDic;

    public Color btnColor_d;
    public Color btnColor_a;

    private void Awake()
    {
        questDic = GameObject.FindWithTag("DataController").GetComponent<QuestDictionary>();
        itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();
        btnColor_d.a = 0;
        btn_90101.enabled = false;
        btn_90102.enabled = false;
        btn_90103.enabled = false;
        btn_90104.enabled = false;
        btn_90101.GetComponent<Image>().color = btnColor_d;
        btn_90102.GetComponent<Image>().color = btnColor_d;
        btn_90103.GetComponent<Image>().color = btnColor_d;
        btn_90104.GetComponent<Image>().color = btnColor_d;
        btnColor_a.a = 255;
        btnColor_a.r = 255;
        btnColor_a.g = 255;
        btnColor_a.b = 255;
    }

    private void LoadBtnInfo(Button btn, int _index)
    {
        QuestInfo questInfo = btn.GetComponent<QuestInfo>();
        QuestInfo findQuestInfo = questDic.findQuestDic[_index];

        questInfo.index = _index;
        questInfo.act = findQuestInfo.act;
        questInfo.title = findQuestInfo.title;
        questInfo.content = findQuestInfo.content;
        questInfo.termsItem = findQuestInfo.termsItem;
        questInfo.termsCount = findQuestInfo.termsCount;
        questInfo.reward = findQuestInfo.reward;

        nameDisplayer.text = questInfo.title;
        //progressDisplayer.text = itemDic.findDic[questInfo.termsItem].mtName + DataController.GetInstance().GetItemNum(_index) + "/" + questInfo.termsCount;
        contentDisplayer.text = questInfo.content;
        rewardDisplayer.text = itemDic.findDic[questInfo.reward].mtName;
    }

    void Update()
    {

        if (90101 <= DataController.GetInstance().GetQuestProcess())
        {
            btn_90101.enabled = true;
            btn_90101.GetComponent<Image>().color = btnColor_a;
            if (90101 == DataController.GetInstance().GetQuestProcess())
            {
                StartCoroutine(Bling(btn_90101));
            }
        }
        if (90102 <= DataController.GetInstance().GetQuestProcess())
        {
            btn_90102.enabled = true;
            btn_90102.GetComponent<Image>().color = btnColor_a;
            if (90102 == DataController.GetInstance().GetQuestProcess())
            {
                StartCoroutine(Bling(btn_90102));
            }
        }
        if (90103 <= DataController.GetInstance().GetQuestProcess())
        {
            btn_90103.enabled = true;
            btn_90103.GetComponent<Image>().color = btnColor_a;
            if (90103 == DataController.GetInstance().GetQuestProcess())
            {
                StartCoroutine(Bling(btn_90103));
            }
        }
        if (90104 <= DataController.GetInstance().GetQuestProcess())
        {
            btn_90104.enabled = true;
            btn_90104.GetComponent<Image>().color = btnColor_a;
            if (90104 == DataController.GetInstance().GetQuestProcess())
            {
                StartCoroutine(Bling(btn_90104));
            }
        }


    }

    IEnumerator Bling(Button btn)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            btn.GetComponent<Image>().color = btnColor_d;
            yield return new WaitForSeconds(0.5f);
            btn.GetComponent<Image>().color = btnColor_a;
        }
    }

    public void Sheep_01()
    {
        int questItem = 0;
        for (int i = 1001; i < 1004; i++)
        {
            questItem += DataController.GetInstance().GetItemNum(i);
        }

        //LoadBtnInfo(btn_90101, 90101);
        //progressDisplayer.text = "별의 원석 " + questItem + "/" + btn_90101.GetComponent<QuestDictionary>().findQuestDic[90101].termsCount;

        nameDisplayer.text = "기본기부터 탄탄하게!(1)";
        progressDisplayer.text = "별의 원석 " + questItem + "/3";
        contentDisplayer.text = "Starry Night의 가장 기초적인 기술인 지구본에서 별의 원석 추출해내는 방법을 알려드리겠습니다. 화면에 보이는 지구본을 클릭하여서 별의 원석을 얻으세요! 클리커를 통해 별의 원석을 획득하시오";
        rewardDisplayer.text = "황금 양";

    }

    public void Sheep_02()
    {
        int questItem = 0;
        for (int i = 1004; i < 1007; i++)
        {
            questItem += DataController.GetInstance().GetItemNum(i);
        }

        //LoadBtnInfo(btn_90102, 90102);
        //progressDisplayer.text = "별의 파편 " + questItem + "/" + btn_90102.GetComponent<QuestDictionary>().findQuestDic[90102].termsCount;

        nameDisplayer.text = "기본기부터 탄탄하게!(2)";
        progressDisplayer.text = "별의 파편 " + questItem + "/1";
        contentDisplayer.text = "아이템을 얻었으니 뭘 해야할지 모르겠다고요? 그렇다면 아이템 하나를 클릭해서 다른 아이템과 합쳐보세요! 더 가격이 높고 좋은 아이템이 생긴답니다. 별의 원석 두 개를 드래그앤 드롭하여서 조합하시오";
        rewardDisplayer.text = "물 한 바가지";
    }

    public void Sheep_03()
    {
        int questItem = 0;
        for (int i = 2001; i < 2007; i++)
        {
            questItem += DataController.GetInstance().GetItemNum(i);
        }

        //LoadBtnInfo(btn_90103, 90103);
        //progressDisplayer.text = "재료 아이템 " + questItem + "/" + btn_90103.GetComponent<QuestDictionary>().findQuestDic[90103].termsCount;

        nameDisplayer.text = "기본기부터 탄탄하게!(3)";
        progressDisplayer.text = "재료 아이템 " + questItem + "/1";
        contentDisplayer.text = "Starry Night의 지구본에서는 별의 원석 뿐만이 아닌 다른 재료도 추출해낼 수 있는 힘이 있답니다. 지구본 아래에 있는 꽃을 클릭하여서 색을 바꾼 다음 지구본을 클릭해서 재료 아이템을 얻으세요";
        rewardDisplayer.text = "날개 달린 신발";
    }

    public void Sheep_04()
    {
        int questItem = 0;
        for (int i = 3001; i < 3007; i++)
        {
            questItem += DataController.GetInstance().GetItemNum(i);
        }

        //LoadBtnInfo(btn_90104, 90104);
        //progressDisplayer.text = "레벨 1 조합 아이템 " + questItem + "/" + btn_90104.GetComponent<QuestDictionary>().findQuestDic[90104].termsCount;

        nameDisplayer.text = "기본기부터 탄탄하게!(4)";
        progressDisplayer.text = "레벨 1 조합 아이템 " + questItem + "/1";
        contentDisplayer.text = "별의 원석만 조합이 가능한건 아니랍니다! 재료아이템과 별의 원석을 조합해서 별의 힘을 지닌 아이템도 얻을 수 있습니다. 별의 원석과 재료를 조합하여서 아이템을 얻으세요!";
        rewardDisplayer.text = "거친 양털";
    }
}
