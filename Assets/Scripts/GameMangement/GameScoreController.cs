using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScoreController : MonoBehaviour
{
    [SerializeField] Text TextScore;
    [SerializeField] Text CurrentTextScore;

    public static readonly string TextFile = @"C:\DucMonsterProject\StackRider\Assets\Resources\score.txt";

    private int _score;

    public static GameScoreController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = int.Parse(ReadFile());
        TextScore.text = _score.ToString();
        CurrentTextScore.text = _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TextScore.text = _score.ToString();
        CurrentTextScore.text = _score.ToString();
    }

    public string ReadFile()
    {
        string score = File.ReadAllText(TextFile);
        return score;
    }

    public void WrieFile()
    {
        File.WriteAllText(TextFile, _score.ToString());
    }

    public int GetScore()
    {
        return _score;
    }

    public void SetScore(int plus)
    {
        _score += plus;
    }
}
