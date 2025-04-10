using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int Type;
  
    public Text statusText;
    public SpriteRenderer renderer;
    
    //public int status였다.
    int status = 0;

    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<SpriteRenderer>();
        //audio = GetComponent<AudioSource>();

        float x = Random.Range(-1.8f, 1.8f);
        float y = Random.Range(3.0f, 4.0f);

        transform.position = new Vector3(x, y, 0);

        Type = Random.Range(1, 7);
        renderer.sprite = Resources.Load<Sprite>($"Bird{Type}");

        //밑에 글을 int status라고 적었었다.
        status = Random.Range(1, GameManager.Instance.totalRain) + GameManager.Instance.totalScore;
        statusText.text = status.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("몬스터 충돌");
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.EndGame();
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.totalScore < status)
            {
                Debug.Log("몬스터가 너무 강해!!");
                GameManager.Instance.EndGame();
            }
            else
            {
                Invoke("Playerscore", 0.005f);
            }
        }
    }

    void Playerscore()
    {
        //피버 상태라면 2배의 점수를 얻는다.
        if(GameManager.Instance.isFever == true)
        {   //단 0보다 작은 수라면 역수로 바꾼다.
            status *= 2;
        }
        else
        {   //피버 상태가 아니라면 피버를 추가한다.
            GameManager.Instance.AddFever();
        }
        Debug.Log("몬스터커트");
        GameManager.Instance.AddScore(status);
        Destroy(this.gameObject);
    }
}
