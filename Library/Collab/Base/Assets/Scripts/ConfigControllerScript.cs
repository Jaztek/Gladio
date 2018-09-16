using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ConfigControllerScript : MonoBehaviour {

    private static GameObject player;
    public GameObject playerPoss;
    static GameObject playerClone;

    void Start() {
        player = GameContextScript.player;
        if (!player)
        {
            print("no tiene player config");
            //Se debe recuperar del context, pero para las pruebas lo cojo directamente de los recursos.
            player = Resources.Load("Prefabs/player") as GameObject;
        }
        // intanciamos el player en la scena
        playerClone = Instantiate(player, playerPoss.transform.position, new Quaternion());
        playerClone.GetComponent<PlayerController>().setFunctional(false);
        playerClone.transform.localScale = playerClone.transform.localScale * 1.5f;
    }

    public static void buildPlayer(){

        GameObject go = new GameObject("habilidades");
        go =Instantiate(go, player.transform);
        go.transform.parent = player.transform;
        GameObject hab1 = Resources.Load("Prefabs/Habilidades/habEscudo") as GameObject;
        hab1 = Instantiate(hab1, go.transform);
        hab1.transform.parent = go.transform;
        GameObject hab2 = Resources.Load("Prefabs/Habilidades/habArrowBasic") as GameObject;
        hab2 = Instantiate(hab2, go.transform);
        hab2.transform.parent = go.transform;


        // reutilizaremos este codigo para guardar el player al cerrar el juego.
        Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Players/player1.prefab");
        PrefabUtility.ReplacePrefab(player, prefab, ReplacePrefabOptions.ConnectToPrefab);

        GameContextScript.setPlayer(player);

       // playerClone = Instantiate(player, player.transform.position, new Quaternion());
       // playerClone.GetComponent<PlayerController>().setFunctional(false);
        //  GameObject.DestroyImmediate(go);

    }
	

}
