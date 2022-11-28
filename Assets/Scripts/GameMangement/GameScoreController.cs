using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScoreController : MonoBehaviour
{
    [SerializeField] Text TextScore;  //show score present that player have in upper right conner
    [SerializeField] Text CurrentTextScore;  //show score present that player have in center canvas
    [SerializeField] Text TextLevel;  //show level at present

    public static readonly string TextFileScore = @".\.\.\Assets\Resources\score.txt";  // link file .txt that contain score of game
    public static readonly string TextFileLevel = @".\.\.\Assets\Resources\level.txt";  // link file .txt that contain level of game

    private int _score;
    private int _level;

    public static GameScoreController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = int.Parse(ReadFile(TextFileScore));    // Read File and asign parameter _score
        TextScore.text = _score.ToString();     // Show score in upper right conner scene
        CurrentTextScore.text = _score.ToString();      // Show score in center of scene
        _level = int.Parse(ReadFile(TextFileLevel).Remove(0, 6));   // Read FIle and assign parameter _level
        TextLevel.text = "LEVEL " + _level.ToString();  // Show Level at present
    }

    // Update is called once per frame
    void Update()
    {
        TextScore.text = _score.ToString();
        CurrentTextScore.text = _score.ToString();
        TextLevel.text = "LEVEL " + _level.ToString();
    }

    /// <summary>
    ///     Read File from resources using path parameter
    /// </summary>
    /// <param name="path">
    ///     Link to file .txt
    /// </param>
    /// <returns>
    ///     String value in file .txt
    /// </returns>
    public string ReadFile(string path)
    {
        string obj = File.ReadAllText(path);
        return obj;
    }

    /// <summary>
    ///     Write _score value to File
    /// </summary>
    public void WrieFileScore()
    {
        File.WriteAllText(TextFileScore, _score.ToString());
    }

    /// <summary>
    ///     Write _level value to File
    /// </summary>
    public void WriteFileLevel()
    {
        File.WriteAllText(TextFileLevel, "LEVEL " + _level.ToString());
    }

    /// <summary>
    ///     Get value of score at present
    /// </summary>
    /// <returns>
    ///     _score value
    /// </returns>
    public int GetScore()
    {
        return _score;
    }

    /// <summary>
    ///     Add a value to the variable _score
    /// </summary>
    /// <param name="plus">
    ///     value added
    /// </param>
    public void SetScore(int plus)
    {
        _score += plus;
    }

    /// <summary>
    ///     Get value of level at present
    /// </summary>
    /// <returns>
    ///     Value of _level variable
    /// </returns>
    public int GetLevel()
    {
        return _level;
    }

    /// <summary>
    ///     Add a value to the variable _level
    /// </summary>
    /// <param name="plus">
    ///     Valued added
    /// </param>
    public void SetLevel(int plus)
    {
        _level += plus;
    }
}
