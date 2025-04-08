using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction = 1f;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Debug.Log("¾È³ç");
        direction = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction *= -1;
            if(direction > 0f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if(transform.position.x > 2.6f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            direction = -0.05f;
        }
        if(transform.position.x < -2.6f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direction = 0.05f;
        }
        transform.position += Vector3.right * direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            anim.SetTrigger("Isbubble");
        }
    }
}
