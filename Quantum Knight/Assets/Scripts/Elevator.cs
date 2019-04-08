using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    Rigidbody2D rb;
    Vector2 start;
    public Vector2 end;
    bool goingUp;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        start = transform.localPosition;
        Debug.Log("This elevator starts at " + start);
        goingUp = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (goingUp)
        {
            if (transform.localPosition.y < end.y)
            {
                rb.velocity = new Vector2(0f, 2f);
            }
            else
            {
                rb.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            if (transform.localPosition.y > start.y)
            {
                rb.velocity = new Vector2(0f, -2f);
            }
            else
            {
                rb.velocity = new Vector2(0f, 0f);
                transform.localPosition = start;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && transform.localPosition.y < end.y)
        {
            goingUp = true;
        }
        else
        {
            goingUp = false;
        }
    }
}
