using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAttackScript   : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Únicamente afecta a la física de los pies
        if (other.CompareTag("Head"))
        {
            PlayerScript playerScript = other.gameObject.GetComponentInParent<PlayerScript>();
            //Stuneamos al enemigo x segundos
            playerScript.canMove = false;
            playerScript.timeStun = Time.time + 5;
        }
    }
}
