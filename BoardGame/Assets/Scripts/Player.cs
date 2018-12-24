using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float in_x, in_y; //axis input
    private Rigidbody rb;
    private int sp;
    private bool special;
    private int hp;
    public int count;
    public float fBase;
    public int needPoint;
    public float jump;
    public int maxHp;
    public string com;
    public Enemy enemy;
    public int gameTimer = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        sp = 0;
        special = false;
        hp = maxHp;
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        gameTimer++;
        if (hp <= 0 && enemy.gameObject.activeSelf) gameObject.SetActive(false);

        if (transform.position.y < -1 || Input.GetKey("r"))
        {
            if (special) special = false;
            Respawn();
        }
        if (!special)
        {
            in_x = Input.GetAxis("Horizontal");
            in_y = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(in_x, 0, in_y) * fBase);
            if (Input.GetButtonDown("Fire1") && sp == needPoint)
            {                
                rb.AddForce(new Vector3(0, jump, 0));
                sp = 0;
                com = "";
                float i;
                while(com.Length < 8)
                {
                    i = Random.Range(0f, 1f);
                    if (i <= 0.25) com += "a";
                    else if (i <= 0.5) com += "w";
                    else if (i <= 0.75) com += "s";
                    else com += "d";
                }
                special = true;
            }
        }

        if (count > 0)
        {
            count--;
            if (count == 0) enemy.rate /= 4;
        }
    }

    public void SpComplete()
    {
        enemy.rate *= 4;
        special = false;
        count = 2000;
    }

    private void Respawn()
    {
        transform.position = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
    }

    public int GetHp() { return hp; }

    public void Damage(int d) { hp -= d; }

    public int GetSp() { return sp; }

    public bool IsSp() { return special; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {
            if (needPoint > sp) sp++;
        }
        else if(special)
        {
            special = false;
        }
        if (collision.gameObject.tag == "Bomb") hp -= 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb") hp -= 10;
    }
}
