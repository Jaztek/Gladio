using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContextScript : MonoBehaviour {

    static public GameObject player;
    // Use this for initialization
    void Awake()
    {
       
        DontDestroyOnLoad(transform.gameObject);
       
        player = Resources.Load("Prefabs/player") as GameObject;
       
    }

    // Update is called once per frame
    void Update () {
		
	}

    public static void setPlayer(GameObject playerClone)
    {
        print(playerClone);
        player = playerClone;
    }

    public static GameObject getPlayer()
    {
        return player;
    }
}
