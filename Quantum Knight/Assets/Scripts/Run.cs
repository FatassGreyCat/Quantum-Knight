using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour {
    Rigidbody2D rb;
    Animator bodyAnim;
    Animator legsAnim;
    public float topSpeed = 10;
    bool facingRight = true;
    bool grounded = true;
    float move;
    GameObject body;
    GameObject legs;
    Vector3 mouse;

    public float energy;
    public GameObject weapon;
    int currentWeapon;

    public List<GameObject> availableWeapons;

    public GameObject reb, leb, rsw, lsw, beam;
    Vector2 ebPosition;
    public float fireRate = 0.01f;
    public float nextFire = 0f;

    // Use this for initialization
    void Start () {
        body = GameObject.Find("Body");
        legs = GameObject.Find("Legs");
        rb = gameObject.GetComponent<Rigidbody2D>();
        bodyAnim = body.GetComponent<Animator>();
        legsAnim = legs.GetComponent<Animator>();
        energy = 0.5f;
        currentWeapon = 0;
    }
	
	// Update is called once per frame
	void Update () {
        move = Input.GetAxis("Horizontal");
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = new Vector2(move*3, rb.velocity.y);
        legsAnim.SetFloat("velocity",Mathf.Abs(rb.velocity.x));
        legsAnim.SetBool("grounded", grounded);
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            attack();
        }
        if (move < 0 && facingRight == true && !(this.tag.Equals("Bar")))
        {
            Flip(legs);
        }
        else if(move > 0 && facingRight == false && !(this.tag.Equals("Bar")))
        {
            Flip(legs);
        }
        if (transform.position.x>=mouse.x && body.transform.localScale.x == 0.75)
        {
            Flip(body);
        }
        else if (transform.position.x < mouse.x && body.transform.localScale.x == -0.75)
        {
            Flip(body);
        }
        if (Input.GetKeyDown("space"))
        {
            if(grounded == true)
            {
                grounded = false;
                rb.velocity = new Vector2(move * topSpeed, 7);
            }
        }
        if (rb.velocity.y == 0)
        {
            grounded = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            dropWeapon(false);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Disruptor") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You picked up a Disruptor");
            dropWeapon(true);
            currentWeapon = 1;
            bodyAnim.SetInteger("weapon", 1);
            weapon = availableWeapons[0];
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Disperser") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You picked up a Disperser");
            dropWeapon(true);
            currentWeapon = 2;
            bodyAnim.SetInteger("weapon", 2);
            weapon = availableWeapons[1];
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Rifle") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You picked up a Rifle");
            dropWeapon(true);
            currentWeapon = 3;
            bodyAnim.SetInteger("weapon", 3);
            weapon = availableWeapons[2];
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("Perforator") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You picked up a Perforator");
            dropWeapon(true);
            currentWeapon = 4;
            bodyAnim.SetInteger("weapon", 4);
            weapon = availableWeapons[3];
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("GasPump") && energy<1f)
        {
            energy += 0.01f;
        }
    }

    void Flip(GameObject bodyPart)
    {
        facingRight = !facingRight;
        Vector3 theScale = bodyPart.transform.localScale;
        theScale.x *= -1;
        bodyPart.transform.localScale = theScale;
    }

    void attack()
    {
        if (currentWeapon == 1 && energy >= 0.01)
        {
            fireRate = 0.4f;
            ebPosition = body.transform.position;

            energy -= 0.01f;
            if (body.transform.localScale.x == 0.75)
            {
                ebPosition += new Vector2(0.9f, 0.45f);
                Instantiate(reb, ebPosition, Quaternion.identity);
            }
            else
            {
                ebPosition += new Vector2(-0.9f, 0.45f);
                Instantiate(leb, ebPosition, Quaternion.identity);
            }
        }
        else if (currentWeapon == 2 && energy >= 0.05)
        {
            fireRate = 0.7f;
            ebPosition = body.transform.position;
            energy -= 0.05f;
            if (body.transform.localScale.x == 0.75)
            {
                ebPosition += new Vector2(2f, 0.4f);
                Instantiate(rsw, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(-50f, 50f));
            }
            else
            {
                ebPosition += new Vector2(-2f, 0.4f);
                Instantiate(lsw, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(50f, 50f));
            }
      
        }
        else if (currentWeapon == 3 && energy >= 0.01)
        {
            fireRate = 0.1f;
            ebPosition = body.transform.position;
            energy -= 0.01f;
            if (body.transform.localScale.x == 0.75)
            {
                ebPosition += new Vector2(1.5f, Random.Range(0.3f,0.5f));
                Instantiate(reb, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(-25f, 0f));
            }
            else
            {
                ebPosition += new Vector2(-1.5f, Random.Range(0.3f, 0.5f));
                Instantiate(leb, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(25f, 0f));
            }

        }
        else if (currentWeapon == 4 && energy >= 0.10)
        {
            fireRate = 1f;
            ebPosition = body.transform.position;
            energy -= 0.1f;
            if (body.transform.localScale.x == 0.75)
            {
                ebPosition += new Vector2(6f, 0.27f);
                Instantiate(beam, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(-50f, 0f));
            }
            else
            {
                ebPosition += new Vector2(-6f, 0.27f);
                Instantiate(beam, ebPosition, Quaternion.identity);
                rb.AddForce(new Vector2(-50f, 0f));
            }
        }
    }

    void dropWeapon(bool cycle)
    {
        ebPosition = transform.position;
        if (!(currentWeapon == 0))
        {
            Debug.Log("You dropped a " + weapon.tag);
            if (cycle == false)
            {
                currentWeapon = 0;
            }
            bodyAnim.SetInteger("weapon", 0);
            Instantiate(weapon, ebPosition + new Vector2((float)(transform.localScale.x*2),0.5f), Quaternion.identity);
        }
    }
}
