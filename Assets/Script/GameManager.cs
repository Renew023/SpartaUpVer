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
        //�ǹ��� 1���� ũ�ų� ���ٸ�,
        if(fever >= 1.0f)
        {
            //�ǹ� ���·� ������.
            isFever = true;
            
        }
        
        //�ǹ������� ��,
        if(isFever == true)
        {
            //Invoke�� ���� ���� �ʴٸ�,
            if(isInvoke == false)
            {   //�ǹ��� ���̴� Invoke�� �����ض�.
                InvokeRepeating("FeverDown", 0f, 0.5f);
                isInvoke = true;
            }
            if(fever < 0.0f)
            {
                isFever = false;
                //�ǹ��� ����Ǹ� Invoke�� ����.
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
