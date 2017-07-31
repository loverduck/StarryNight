using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sale : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Material")
        {
            Debug.Log("Trigger");
            DataController.GetInstance().AddGold(col.gameObject.GetComponent<ItemCost>().GetCost());
            DataController.GetInstance().SubItemCount();
            Destroy(col.gameObject);

            CameraController.focusOnItem = false;
        }
    }
}
