using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject rain;
    public GameObject EndPanel;

    public Text TotalScoreTxt;


    int totalScore;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        InvokeRepeating("MakeRain", 0f, 1f);
    }

    void Update()
    {
        
    }

    void MakeRain()
    {
        Instantiate(rain);
    }

    

    public void AddScore(int score)
    {
        totalScore += score;
        TotalScoreTxt.text = totalScore.ToString();
    }
}
