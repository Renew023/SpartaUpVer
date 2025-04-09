using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject rain;
    public GameObject monster;
    public GameObject EndPanel;

    public Text TotalScoreTxt;
    public Text CurrentScoreTxt;
    public Text BestScoreTxt;
    public RectTransform Front;

    public bool isFever = false;

    public int totalScore;
    public int totalRain;

    [SerializeField]
    float fever = 0.0f;
    [SerializeField]
    float Speed = 1.0f;
    string key = "bestScore";

    bool isInvoke = false;
    public AudioSource audio;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        InvokeRepeating("MakeRain", 0f, 1.0f);
    }

    void Update()
    {
        
        //피버가 1보다 크거나 같다면,
        if(fever >= 1.0f)
        {
            if(isFever == false)
            {
                Debug.Log("0");
                //피버 상태로 만들어라.
                isFever = true;
                //AudioManager.Instance.FeverChange();
            }
        }
        
        //피버상태일 때,
        if(isFever == true)
        {
            //Invoke가 켜져 있지 않다면,
            if(isInvoke == false)
            {   //피버를 줄이는 Invoke를 실행해라.
                InvokeRepeating("FeverDown", 1f, 0.5f);
                isInvoke = true;
            }
            if(fever < 0.0f)
            {
                isFever = false;
                fever = 0.0f;
                AudioManager.Instance.musicChange();
                //피버가 종료되면 Invoke를 꺼라.
                CancelInvoke("FeverDown");
                isInvoke = false;
            }
        }
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

    public void EndGame()
    {
        Time.timeScale = 0.0f;
        EndPanel.SetActive(true);
        CurrentScoreTxt.text = totalScore.ToString();

        if (PlayerPrefs.HasKey(key))
        {   //최고 점수 가져오기
            int best = PlayerPrefs.GetInt(key);
            if (best < totalScore)
            {
                PlayerPrefs.SetInt(key, totalScore);
                BestScoreTxt.text = totalScore.ToString();
            }
            else
            {
                BestScoreTxt.text = best.ToString();
            }
        }
        else
        {
            PlayerPrefs.SetInt(key, totalScore);
            BestScoreTxt.text = totalScore.ToString();
        }
    }

    void MakeRain()
    {
        Instantiate(rain);
        totalRain += 1;
       
        if (totalRain % 10 == 0)
        {
            MakeMonster();
            InvokeSpeed();
        }
    }

    void MakeMonster()
    {
        Instantiate(monster);
    }

    void InvokeSpeed()
    {
        CancelInvoke("MakeRain");
        Speed = Speed - Speed * 0.3f;
        InvokeRepeating("MakeRain", 1f, Speed);
    }
    
}
