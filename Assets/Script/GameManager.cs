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
        
        //�ǹ��� 1���� ũ�ų� ���ٸ�,
        if(fever >= 1.0f)
        {
            if(isFever == false)
            {
                Debug.Log("0");
                //�ǹ� ���·� ������.
                isFever = true;
                //AudioManager.Instance.FeverChange();
            }
        }
        
        //�ǹ������� ��,
        if(isFever == true)
        {
            //Invoke�� ���� ���� �ʴٸ�,
            if(isInvoke == false)
            {   //�ǹ��� ���̴� Invoke�� �����ض�.
                InvokeRepeating("FeverDown", 1f, 0.5f);
                isInvoke = true;
            }
            if(fever < 0.0f)
            {
                isFever = false;
                fever = 0.0f;
                AudioManager.Instance.musicChange();
                //�ǹ��� ����Ǹ� Invoke�� ����.
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
        {   //�ְ� ���� ��������
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
