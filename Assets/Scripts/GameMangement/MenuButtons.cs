using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        Upgrade(name);
    }

    public void LoadSceneAndDoubleScore(string name)
    {
        GameScoreController.Instance.SetScore(GameScoreController.Instance.GetScore());
        Upgrade(name);
    }

    private void Upgrade(string name)
    {
        GameScoreController.Instance.SetLevel(1);
        GameScoreController.Instance.WriteFileLevel();
        SceneManager.LoadScene(name);
    }
}
