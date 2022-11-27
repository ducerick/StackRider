using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePopup : MonoBehaviour
{
    [SerializeField] GameObject PopupMenu;  //This is a object Game Popup when player dead
    [SerializeField] Text ButtonContinueOrAgain; // This is a button Continue or try again when people play successfull or failed
    [SerializeField] Button ButtonAdv;  //This is a button Advertisement when people want to double coin

    public static GamePopup Instance;   // Singleton Pattern

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        PopupMenu.SetActive(false);
    }

    /// <summary>
    ///     Set Game PopUp active equal true
    /// </summary>
    public void SetPopup()
    {
        PopupMenu.SetActive(true);
    }
    
    /// <summary>
    ///     Set text of Button is continue or try again
    /// </summary>
    /// <param name="text">
    ///     Text of button
    /// </param>
    public void SetText(string text)
    {
        ButtonContinueOrAgain.text = text;
    }

    /// <summary>
    ///     Set Button Adv active equal to false when player start playing again or continue
    /// </summary>
    public void DeActivateButtonAdv()
    {
        ButtonAdv.gameObject.SetActive(false);
    }
}
