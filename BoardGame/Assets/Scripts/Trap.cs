using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public int timer = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0)
        {
            timer--;
            if (timer == 0) gameObject.SetActive(false);
        }
	}
}
