using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PantallaInicioScript : MonoBehaviour
{

    public GameObject camera;
    private static GameObject player;
    static GameObject playerClone;
    private  List<GameObject> btHabilidades;
    private  GameObject btHab;
    private  GameObject btReturn;
    private  GameObject btStart;
    private  GameObject btOption;
    private GameObject btLoot;
    private static GameObject gridPane;

    private float sizeGridPane;

    private static GameObject paHabilidades;

    void Start()
    {
        gridPane = GameObject.Find("gridPane");
        btOption = GameObject.Find("btOptions");
        btOption.GetComponentInChildren<Button>().onClick.AddListener(optionListener);
        player = SaveLoad.Load();
        if (!player)
        {
            //Si no existe cargamos el player del resources
            player = Resources.Load("Prefabs/Players/player") as GameObject;
        }

        // Instanciamos el player en la escena
        playerClone = Instantiate(player, new Vector3(0f, -2.5f, 0f), new Quaternion());
        playerClone.GetComponent<PlayerController>().setFunctional(false);
        Destroy(playerClone.GetComponent<Rigidbody2D>());
        playerClone.transform.localScale = playerClone.transform.localScale;

        btHabilidades = new List<GameObject>(GameObject.FindGameObjectsWithTag("Habilidad"));
        btHab = GameObject.Find("habilidadesButton");
        btReturn = GameObject.Find("returnButton");
        btStart = GameObject.Find("startButton");
        paHabilidades = GameObject.Find("paHabilidades");
        btLoot = GameObject.Find("btLoot");

        // Añadimos los listeners a los botones
        addListeners();

        // Volvemos activos solamente los botones "habilidades" y "start"
        initButtons();

        buildPanelHabilidades(); 

    }

  

    void addListeners()
    {
        btHab.GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
        btReturn.GetComponentInChildren<Button>().onClick.AddListener(returnToInicial);
        btStart.GetComponentInChildren<Button>().onClick.AddListener(startGame);
        btOption.GetComponentInChildren<Button>().onClick.AddListener(optionListener);
        btLoot.GetComponentInChildren<Button>().onClick.AddListener(lootListener);
    }

    void optionListener()
    {
        SaveLoad.clearSave();
    }

    void lootListener()
    {
        SaveLoad.chargeLoot();
    }

    void returnToInicial()
    {
        buildPlayer();
        btHabilidades.ForEach(h => h.GetComponentInChildren<BtHabInicioScript>().btLimpiarListener());
        initButtons();

        camera.GetComponent<CamaraInicioScript>().goToInicio();
    }

    void initButtons()
    {
        btHabilidades.ForEach(h => h.SetActive(false));
        btReturn.SetActive(false);
        btHab.SetActive(true);
        btStart.SetActive(true);
        paHabilidades.SetActive(false);
    }

    void habButtonOnClick()
    {
        btHabilidades.ForEach(h => h.SetActive(true));

        List<HabilidadTemplate> habSeleccionadas = SaveLoad.getPlayerTemp().getHabilidadesConjunto1();
        btReturn.SetActive(true);
        btHab.SetActive(false);
        btStart.SetActive(false);

        habSeleccionadas.ForEach(h => {   

            btHabilidades.ForEach(btHab =>
            {
                if(btHab.GetComponent<BtHabInicioScript>().prioridad == h.prioridad)
                {
                    btHab.GetComponent<BtHabInicioScript>().pintarHabilidadSeleccionada(h);
                }
            });

        });

        camera.GetComponent<CamaraInicioScript>().goToHabilidades();
      }

    public void buildPlayer()
    {

        List<HabilidadTemplate> habSeleccionadas = new List<HabilidadTemplate>();

        foreach (GameObject hab in btHabilidades)
        {
            if (hab.GetComponent<BtHabInicioScript>().getHabilidad() != null) {
                habSeleccionadas.Add(hab.GetComponent<BtHabInicioScript>().getHabilidad());
             }
        }

        SaveLoad.saveHabilitiesConjunto1(habSeleccionadas);

    }

    void startGame()
    {
        SceneManager.LoadScene("BattleScene");
    }


    // panel de habilidades|||||
    public static List<HabilidadTemplate> buildPanelHabilidades()
    {
        GameObject single = Resources.Load("Prefabs/UI/btSingle") as GameObject;
        List<HabilidadTemplate> habilidades = SaveLoad.getHabilities();
        //Ordena los botones alfabeticamente
        habilidades.Sort((x, y) => x.nombre.CompareTo(y.nombre));
        if (habilidades != null)
        {
            foreach (HabilidadTemplate hab in habilidades)
            {
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(gridPane.transform);

                singleClone.transform.localPosition = new Vector3(0, 0, 0);

                singleClone.GetComponent<PaHabScript>().inicializar(hab.nombre, hab.damage, hab.cooldown, hab.maxProximity, hab);
            }
        }
 
        return habilidades;
    }

    void Update()
    {
        float posicionYGridPane = gridPane.GetComponentInChildren<RectTransform>().anchoredPosition.y;
        float posicionXGridPane = gridPane.GetComponentInChildren<RectTransform>().anchoredPosition.x;
        if (gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y < 300)
        {
            sizeGridPane = gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y;
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, (300 - sizeGridPane) / 2);
        }
        else if (gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y != sizeGridPane)
        {
            sizeGridPane = gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y;
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, -(sizeGridPane - 300) / 2);
        }
        else if (posicionYGridPane < -((sizeGridPane - 300) / 2))
        {
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, -(sizeGridPane - 300) / 2);
        }
        else if (posicionYGridPane > ((sizeGridPane - 300) / 2))
        {
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, (sizeGridPane - 300) / 2);
        }
    }

    
}
