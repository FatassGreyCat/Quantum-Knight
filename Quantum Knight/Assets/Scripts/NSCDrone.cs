using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSCDrone : MonoBehaviour {
    GameObject targetedPlayer;
    Rigidbody2D rb, rbTarget;
    Animator anim;
    int life;
    Vector3 theScale;
    // Use this for initialization
    void Start () {
        targetedPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rbTarget = targetedPlayer.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        life = 100;
        theScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (life<=0)
        {
            rb.gravityScale = 0.5f;
            anim.SetBool("destroy",true);
            Destroy(gameObject,0.8f);
        }
        else{
            if (life < 100)
            {
                ChaseTargetedPlayer();
            }
            else
            {
                rb.velocity = new Vector2(0f, 0.2f * Mathf.Cos(Time.time));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shockwave"))
        {
            life -= 25;
        }
        if (collision.gameObject.CompareTag("SmallEnergyBurst"))
        {
            life -= 5;
        }
        if (collision.gameObject.CompareTag("Beam"))
        {
            life -= 150;
        }
    }
    void ChaseTargetedPlayer()
    {
        theScale = transform.localScale;
        if (rbTarget.position.x < transform.position.x)
        {
            theScale.x = 1;
            transform.localScale = theScale;
        }
        if (rbTarget.position.x >= rb.position.x)
        {
            theScale.x = -1;
            transform.localScale = theScale;
        }
    }
}
