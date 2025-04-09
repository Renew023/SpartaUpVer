using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    AudioSource audio;
    AudioClip clip;

    float direction;

    public Animator anim;
    Vector2 DefaultScale = new Vector2(1.0f, 1.0f);
    Vector2 DefaultPosition;

    bool isFever = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Debug.Log("�ȳ�");
        direction = 0.05f;
        DefaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction *= -1;
            if(direction > 0f)
            {
                Debug.Log("1����");
                DefaultScale = new Vector2(Mathf.Abs(DefaultScale.x) * 1, DefaultScale.y);
                transform.localScale = DefaultScale;
            }
            else
            {
                Debug.Log("2����");
                DefaultScale = new Vector2(Mathf.Abs(DefaultScale.x)* -1, DefaultScale.y);
                transform.localScale = DefaultScale;
            }
        }

        if(transform.localPosition.x > 2.0f)
        {
            Debug.Log("3����");

            //�Ʒ� �ڵ� ���� �� ������ ���ų� ���ڸ��� ����.
            direction = 0.05f;
            Flip();
        }
        if(transform.localPosition.x < -2.0f)
        {
            Debug.Log("4����");

            //�Ʒ� �ڵ� ���� �� ������ ���ų� ���ڸ��� ����.
            direction = -0.05f;

            Flip();
        }

        //�ǹ� �� ���� ����
        //�ǹ��� �ƴ� ��
        if(isFever == false)
        {   //�ǹ��� �Ǿ����� Ȯ��
            if (GameManager.Instance.isFever == true)
            {
                Debug.Log("1");//�ǹ���� ������*2, ��ġ�� +0.75
                DefaultScale = DefaultScale * 2.0f;
                transform.position = new Vector2(transform.position.x, DefaultPosition.y + 0.75f);
                transform.localScale = DefaultScale;
                isFever = true;
            }
        }
        else //�ǹ��� ��
        {   //���ӸŴ����� �ǹ��� Ǯ�ȴ��� Ȯ��
            if (GameManager.Instance.isFever == false)
            {   //�ǹ��� ����Ǹ� ������/2, ��ġ�� �⺻������
                DefaultScale = DefaultScale / 2.0f;
                transform.position = new Vector2(transform.position.x, DefaultPosition.y);
                transform.localScale = DefaultScale;
                isFever = false;
            }
        }

        transform.position += Vector3.left * direction;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            audio.PlayOneShot(clip);
            anim.SetTrigger("Isbubble");
        }
    }

    void Flip()
    {
        DefaultScale = new Vector2(DefaultScale.x * -1, DefaultScale.y);
        transform.localScale = DefaultScale;
    }
}
