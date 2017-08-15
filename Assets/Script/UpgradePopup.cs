using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePopup : MonoBehaviour
{
    private GameObject upgrade;

    private void Awake()
    {
        upgrade = GameObject.Find("Upgrade Panel");
        upgrade.SetActive(false);
    }

    public void EnterUpgrade()
    {
        transform.SetAsLastSibling();
        upgrade.SetActive(true);
    }

    public void ExitUpgrade()
    {
        upgrade.SetActive(false);
    }
}