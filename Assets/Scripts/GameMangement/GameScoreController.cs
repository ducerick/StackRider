using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScoreController : MonoBehaviour
{
    [SerializeField] Text TextScore;
    [SerializeField] Text CurrentTextScore;
    [SerializeField] Text TextLevel;

    public static readonly string TextFileScore = @"C:\DucMonsterProject\StackRider\Assets\Resources\score.txt";
    public static readonly string TextFileLevel = @"C:\DucMonsterProject\StackRider\Assets\Resources\level.txt";

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
        _score = int.Parse(ReadFile(TextFileScore));
        TextScore.text = _score.ToString();
        CurrentTextScore.text = _score.ToString();
        _level = int.Parse(ReadFile(TextFileLevel).Remove(0, 6));
        TextLevel.text = "LEVEL " + _level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TextScore.text = _score.ToString();
        CurrentTextScore.text = _score.ToString();
        TextLevel.text = "LEVEL " + _level.ToString();
    }

    public string ReadFile(string path)
    {
        string obj = File.ReadAllText(path);
        return obj;
    }

    public void WrieFileScore()
    {
        File.WriteAllText(TextFileScore, _score.ToString());
    }

    public void WriteFileLevel()
    {
        File.WriteAllText(TextFileLevel, "LEVEL " + _level.ToString());
    }

    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int plus)
    {
        _score += plus;
    }

    public int GetLevel()
    {
        return _level;
    }

    public void SetLevel(int plus)
    {
        _level += plus;
    }
}
