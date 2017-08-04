﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    private void OnMouseDown()
    {
        //CameraController.focusOnItem = true;
        //Debug.Log("focusOnItem : " + CameraController.focusOnItem);
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }

    private void OnMouseUp()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        StartCoroutine(CheckCollision());
    }

    IEnumerator CheckCollision()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<BoxCollider2D>().isTrigger = false;

        //CameraController.focusOnItem = false;
        //Debug.Log("focusOnItem : " + CameraController.focusOnItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemInfo collItemInfo = collision.GetComponent<ItemInfo>();

        if (collision.tag == "Material" && collision.isTrigger && !collItemInfo.checkDestroy)
        {
            // 조합표에 있는 조합식인지 검색한다.
            ItemDictionary itemDic = GameObject.FindWithTag("DataController").GetComponent<ItemDictionary>();

            ItemInfo myItemInfo = GetComponent<ItemInfo>();

            int key1 = myItemInfo.index;
            int key2 = collItemInfo.index;
            List<int> resultList = itemDic.FindCombine(key1, key2);

            if (resultList != null)
            {
                // 조합표에 있다면 충돌 당한 물체를 결과 재료로 바꾸어준다.
                //Debug.Log("result != 0");

                // 조합 결과 개수를 얻어온다.
                int resultNum = resultList.Count;

                ItemInfo findItemInfo = itemDic.FindItem(resultList[Random.Range(0, resultNum)]);

                myItemInfo.index = findItemInfo.index;
                myItemInfo.mtName = findItemInfo.mtName;
                myItemInfo.group = findItemInfo.group;
                myItemInfo.grade = findItemInfo.grade;
                myItemInfo.sellPrice = findItemInfo.sellPrice;
                myItemInfo.description = findItemInfo.description;
                myItemInfo.imagePath = findItemInfo.imagePath;

                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemDic.findDic[myItemInfo.index].imagePath);

                // 충돌한 물체를 가지고 있는 재료 dictionary에서 삭제한다.
                DataController.GetInstance().DeleteItem(key2);
                DataController.GetInstance().DeleteItem(key1);

                DataController.GetInstance().InsertItem(key1);

                // 조합 후 충돌한 물체를 파괴한다.
                collItemInfo.checkDestroy = true;

                Destroy(collision.gameObject);

                DataController.GetInstance().SubItemCount();
            }
            //else
            //{
            //    Debug.Log("result == 0");
            //}

            // 조합표에 없다면 그냥 무시한다.
        }

        StartCoroutine(CheckCollision());
    }
}