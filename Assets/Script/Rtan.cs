using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;
        Debug.Log("안녕");
        direction = 0.02f;
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
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Debug.Log("2감지");
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if(transform.localPosition.x > 2.0f)
        {
            Debug.Log("3감지");
            direction = 0.05f;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(transform.localPosition.x < -2.0f)
        {
            Debug.Log("4감지");
            direction = -0.05f;
            transform.localScale = new Vector3(-1, 1, 1);
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
    
}
