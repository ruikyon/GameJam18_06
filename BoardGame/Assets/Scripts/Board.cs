using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private float in_x, in_y; //axis input
    public int timer = 0;
    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer--;
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            if (timer == 0)
            {
                if (player.transform.position.y < 0.75) player.Damage(15);
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
