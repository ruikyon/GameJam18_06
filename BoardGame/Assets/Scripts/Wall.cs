using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public int timer = 0;
    public bool dir;
    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0)
        {
            timer--;
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            if (timer == 0)
            {
                if (dir)
                {
                    if (Mathf.Abs(player.transform.position.z - transform.position.z) < 2) player.Damage(15);
                }
                else
                {
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2) player.Damage(15);
                }
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            
        }
	}
}
