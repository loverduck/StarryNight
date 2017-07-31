using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

    public void OnImgBtnClick()
    {
        transform.SetAsLastSibling();
    }

    public void OnExitBtnClick()
    {
        transform.SetAsFirstSibling();
    }
}
