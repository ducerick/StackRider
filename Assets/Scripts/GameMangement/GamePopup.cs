using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopup : MonoBehaviour
{
    [SerializeField] GameObject PopupMenu;
    [SerializeField] Text ButtonContinueOrAgain;
    [SerializeField] Button ButtonAdv;

    public static GamePopup Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        PopupMenu.SetActive(false);
    }

    public void SetPopup()
    {
        PopupMenu.SetActive(true);
    }

    public void SetText(string text)
    {
        ButtonContinueOrAgain.text = text;
    }

    public void DeActivateButtonAdv()
    {
        ButtonAdv.gameObject.SetActive(false);
    }
}
