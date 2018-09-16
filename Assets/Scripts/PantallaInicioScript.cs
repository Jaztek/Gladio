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


    private void Awake()
    {
        player = SaveLoad.Load();
    }

    void Start()
    {
        // QueryMaster.scriptInsertWeapon();

        uIController = GetComponent<UIController>();
      
        if (!player)
        {
            //Si no existe cargamos el player del resources
            player = Resources.Load("Prefabs/Players/player") as GameObject;
        }

        // Instanciamos el player en la escena
        playerClone = Instantiate(player, new Vector3(-0, -2.5f, 0f), new Quaternion());
        playerClone.GetComponent<PlayerController>().setFunctional(false);
        Destroy(playerClone.GetComponent<Rigidbody2D>());
        playerClone.transform.localScale = playerClone.transform.localScale;


        // Volvemos activos solamente los botones "habilidades" y "start"
        buildPanelHabilidades();
        buildPaneItems();
        buildPaneWeapons();


    } 

    // panel de habilidades|||||
    public void buildPanelHabilidades()
    {
        List<HabilidadTemplate> habilidades = SaveLoad.getHabilities();

        uIController.buildPanelHabilidades(habilidades);
      
    }

    public void buildPaneItems()
    {
        List<ItemTemplate> items = SaveLoad.getItems();

        uIController.buildPanelItems(items);

    }

    public void buildPaneWeapons()
    {
        List<WeaponTemplate> weap = SaveLoad.getWeapons();

        uIController.buildPanelWeapon(weap);

    }
}
