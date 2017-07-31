using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateItem2 : MonoBehaviour {

    public GameObject prefab;

    public Image img;
    public UnityEngine.UI.Button btn;
    public float cooltime = 10.0f;
    public bool disableOnStart = false;
    float leftTime = 10.0f;

    

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
        if (leftTime > 0)
        {
            btn.enabled = false;
            leftTime -= Time.deltaTime;
            if (leftTime < 0)
            {
                leftTime = 0;
                if (btn)
                {
                    btn.enabled = true;
                }
                    
            }
            float ratio = 1.0f - (leftTime / cooltime);
            if (img)
                img.fillAmount = ratio;
        }
    }

    public bool CheckCooltime()
    {
        if (leftTime > 0)
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

            leftTime = cooltime;
            btn.enabled = false;
            
            DataController.GetInstance().AddItemCount();
        }
    }

}
