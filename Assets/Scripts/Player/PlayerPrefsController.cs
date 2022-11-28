using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsController : MonoBehaviour
{
    public static PlayerPrefsController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    #region Add
    public void AddScore()
    {
        int score = GameScoreController.Instance.GetScore();
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public void AddLevel()
    {
        int level = GameScoreController.Instance.GetLevel();
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }

    #endregion
}