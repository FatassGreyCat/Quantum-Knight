using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour {
    Rigidbody2D rb;
    Animator anim;
    public GameObject shell;
    float direction = 1f;
    int life;
    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = 1f;
        life = 50;
        StartCoroutine(MoveAround());
    }

    // Update is called once per frame
    void Update() {
        if(life>0){
            transform.localScale = new Vector2(2f * direction, 2f);
            rb.velocity = new Vector2(-0.5f * direction, 0f);
        }
        if (life<=0)
        {
            Instantiate(shell,transform.position,Quaternion.identity);
            Destroy(gameObject,0f);
        }
    }

    IEnumerator MoveAround()
    {
        while (life>0) {
            direction *= -1f;
            yield return new WaitForSeconds(3);
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
}
