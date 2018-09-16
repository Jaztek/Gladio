using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantallaInicioScript : MonoBehaviour
{

   
    private static GameObject player;
    static GameObject playerClone;

    private UIController uIController;
   
    void Start()
    {
        uIController = GetComponent<UIController>();
         player = SaveLoad.Load();
        if (!player)
        {
            //Si no existe cargamos el player del resources
            player = Resources.Load("Prefabs/Players/player") as GameObject;
        }

        // Instanciamos el player en la escena
        playerClone = Instantiate(player, new Vector3(-0.45f, -2.5f, 0f), new Quaternion());
        playerClone.GetComponent<PlayerController>().setFunctional(false);
        Destroy(playerClone.GetComponent<Rigidbody2D>());
        playerClone.transform.localScale = playerClone.transform.localScale;


        // Volvemos activos solamente los botones "habilidades" y "start"
        buildPanelHabilidades();



    } 

    public void buildPlayer(List<HabilidadTemplate> habilidades)
    {
        SaveLoad.saveHabilitiesConjunto1(habilidades);
    }

    // panel de habilidades|||||
    public void buildPanelHabilidades()
    {
        GameObject single = Resources.Load("Prefabs/UI/btSingle") as GameObject;
        List<HabilidadTemplate> habilidades = SaveLoad.getHabilities();

        uIController.buildPanelHabilidades(habilidades);
      
    }

  
}
