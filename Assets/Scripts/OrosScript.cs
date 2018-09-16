using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrosScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = Convert.ToString(SaveLoad.getPlayerTemp().getOro());
	}
	
    public void comprar(int cantidad)
    {
        int total = Convert.ToInt32(GetComponentInChildren<Text>().text) + cantidad;
        SaveLoad.getPlayerTemp().setOro(total);
        GetComponentInChildren<Text>().text = Convert.ToString(total);
    }
}
