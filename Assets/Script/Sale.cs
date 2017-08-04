using UnityEngine;

public class Sale : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Material")
        {
            //Debug.Log("Trigger");
            DataController.GetInstance().AddGold(col.GetComponent<ItemInfo>().sellPrice);
            DataController.GetInstance().SubItemCount();

            Destroy(col.gameObject);

            //CameraController.focusOnItem = false;
        }
    }
}