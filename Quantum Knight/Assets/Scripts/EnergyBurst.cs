using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBurst : MonoBehaviour {

    public float xvelocity = 0;
    Rigidbody2D rb;
    Animator anim;
    public float decayTime;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("disintegrate",1f);
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(xvelocity, 0);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        disintegrate();
    }

    void disintegrate()
    {
        anim.SetBool("destroy", true);
        Destroy(gameObject, decayTime);
    }
}
