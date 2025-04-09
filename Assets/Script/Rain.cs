using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    float size = 1.0f;
    int score = 1;

    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        float x = Random.Range(-2.4f, 2.4f);
        float y = Random.Range(3.0f, 5.0f);

        transform.position = new Vector3(x, y, 0);

        int Type = Random.Range(1, 5);

        if(Type == 1)
        {
            size = 0.8f;
            score = 1;
            renderer.color = new Color(100 / 255f, 100 / 255f, 1f, 1f);
        }
        else if(Type == 2)
        {
            size = 1.0f;
            score = 2;
            renderer.color = new Color(130 / 255f, 133 / 255f, 1f, 1f);
        }
        else if(Type == 3)
        {
            size = 1.2f;
            score = 3;
            renderer.color = new Color(150 / 255f, 150 / 255f, 1f, 1f);
        }
        else if(Type == 4)
        {
            size = 0.8f;
            score = -5;
            renderer.color = new Color(1f, 100 / 255f, 1f, 1f);
        }

        transform.localScale = new Vector3(size, size, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("Playerscore", 0.005f);
        }
    }

    void Playerscore()
    {
        //피버 상태라면 2배의 점수를 얻는다.
        if(GameManager.Instance.isFever == true)
        {   //단 0보다 작은 수라면 역수로 바꾼다.
            if(score < 0)
            {
                score *= -1;
            }
            score *= 2;
        }
        else
        {   //피버 상태가 아니라면 피버를 추가한다.
            GameManager.Instance.AddFever();
        }
        
        GameManager.Instance.AddScore(score);
        Destroy(this.gameObject);
    }
}
