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
    public RectTransform Front;

    public bool isFever = false;

    int totalScore;
    float fever = 0.0f;
    bool isInvoke = false;

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
        //피버가 1보다 크거나 같다면,
        if(fever >= 1.0f)
        {
            //피버 상태로 만들어라.
            isFever = true;
            
        }
        
        //피버상태일 때,
        if(isFever == true)
        {
            //Invoke가 켜져 있지 않다면,
            if(isInvoke == false)
            {   //피버를 줄이는 Invoke를 실행해라.
                InvokeRepeating("FeverDown", 0f, 0.5f);
                isInvoke = true;
            }
            if(fever < 0.0f)
            {
                isFever = false;
                //피버가 종료되면 Invoke를 꺼라.
                CancelInvoke("FeverDown");
                isInvoke = false;
            }
        }
    }

    void MakeRain()
    {
        Instantiate(rain);
    }

    public void AddFever()
    {
        fever += 0.2f;
        Front.localScale = new Vector3(fever / 1.0f, 1.0f, 1.0f);
    }
    

    public void AddScore(int score)
    {
        totalScore += score;
        TotalScoreTxt.text = totalScore.ToString();
    }

    public void FeverDown()
    {
        fever -= 0.05f;
        Front.localScale = new Vector3(fever / 1.0f, 1.0f, 1.0f);
    }
}
