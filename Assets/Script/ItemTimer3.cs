using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimer3 : MonoBehaviour {

    public GameObject prefab;
    public Text timeDisplayer;
    public Image img;
    public UnityEngine.UI.Button btn;
    float cooltime = 300.0f;
    public bool disableOnStart = false;
    private int sec;
    private int sec_1;
    private int sec_10;
    private int min;



    void Start()
    {
        if (img == null)
            img = gameObject.GetComponent<Image>();
        if (btn == null)
            btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        //if (disableOnStart)
        //    ResetCooltime();
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
            timeDisplayer.text = "0" + min + ":" + sec_10 + sec_1;

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

            GameObject item = Instantiate(prefab, new Vector3(-621, 772, -4), Quaternion.identity);
            item.GetComponent<BoxCollider2D>().isTrigger = false;
            //Instantiate(prefab, new Vector3(39, 720, 0), Quaternion.identity).transform.SetParent(GameObject.Find("Canvas").transform, false);

            DataController.GetInstance().SetLeftTimer3(cooltime);
            btn.enabled = false;

            DataController.GetInstance().AddItemCount();
        }
    }

}
