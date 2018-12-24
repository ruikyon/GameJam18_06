using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition -= new Vector3(0, speed, 0);
        if (transform.localPosition.y < 0) gameObject.SetActive(false);
	}
}
