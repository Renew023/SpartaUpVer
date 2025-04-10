using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int Type;
  
    public Text statusText;
    public SpriteRenderer renderer;
    
    //public int status����.
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

        //�ؿ� ���� int status��� ��������.
        status = Random.Range(1, GameManager.Instance.totalRain) + GameManager.Instance.totalScore;
        statusText.text = status.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���� �浹");
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.EndGame();
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.totalScore < status)
            {
                Debug.Log("���Ͱ� �ʹ� ����!!");
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
        //�ǹ� ���¶�� 2���� ������ ��´�.
        if(GameManager.Instance.isFever == true)
        {   //�� 0���� ���� ����� ������ �ٲ۴�.
            status *= 2;
        }
        else
        {   //�ǹ� ���°� �ƴ϶�� �ǹ��� �߰��Ѵ�.
            GameManager.Instance.AddFever();
        }
        Debug.Log("����ĿƮ");
        GameManager.Instance.AddScore(status);
        Destroy(this.gameObject);
    }
}
