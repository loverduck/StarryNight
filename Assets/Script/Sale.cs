using UnityEngine;

public class Sale : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Material")
        {
            ItemInfo itemInfo = col.GetComponent<ItemInfo>();

            DataController.GetInstance().AddGold((ulong)itemInfo.sellPrice);
            DataController.GetInstance().SubItemCount();
            DataController.GetInstance().DeleteItem(itemInfo.index);

            Destroy(col.gameObject);

            //CameraController.focusOnItem = false;
        }
    }
}