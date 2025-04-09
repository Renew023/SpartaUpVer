using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rtan : MonoBehaviour
{

    float direction;

    public Animator anim;
    Vector2 DefaultScale = new Vector2(1.0f, 1.0f);
    Vector2 DefaultPosition;

    bool isFever = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Debug.Log("안녕");
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
                Debug.Log("1감지");
                Flip();
            }
            else
            {
                Debug.Log("2감지");
                Flip();
            }
        }

        if(transform.localPosition.x > 2.0f)
        {
            Debug.Log("3감지");

            //아래 코드 변경 시 벽으로 들어가거나 제자리에 있음.
            direction = 0.05f;
            Flip();
        }
        if(transform.localPosition.x < -2.0f)
        {
            Debug.Log("4감지");

            //아래 코드 변경 시 벽으로 들어가거나 제자리에 있음.
            direction = -0.05f;

            Flip();
        }

        //피버 및 방향 설정
        //피버가 아닐 때
        if(isFever == false)
        {   //피버가 되었는지 확인
            if (GameManager.Instance.isFever == true)
            {   //피버라면 스케일*2, 위치값 +0.75
                DefaultScale = DefaultScale * 2.0f;
                transform.position = new Vector2(transform.position.x, DefaultPosition.y + 0.75f);
                transform.localScale = DefaultScale;
                isFever = true;
            }
        }
        else //피버일 때
        {   //게임매니저의 피버가 풀렸는지 확인
            if (GameManager.Instance.isFever == false)
            {   //피버가 해재되면 스케일/2, 위치값 기본값으로
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
            anim.SetTrigger("Isbubble");
        }
    }

    void Flip()
    {
        DefaultScale = new Vector2(DefaultScale.x * -1, DefaultScale.y);
        transform.localScale = DefaultScale;
    }
}
