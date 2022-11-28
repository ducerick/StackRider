using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStaticController : MonoBehaviour
{
    public static CanvasStaticController instance;

    public GamePopup GamePopupController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

}
