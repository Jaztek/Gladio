using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // Únicamente afecta a la física de la cabeza
        if (other.CompareTag("Head"))
        {
            print("cabesa");
            PlayerScript enemy = other.gameObject.GetComponentInParent<PlayerScript>();
            //Stuneamos al enemigo x segundos
            enemy.stuned(GetComponentInParent<PlayerScript>().getPowerStun());
        }
    }
}
