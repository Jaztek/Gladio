using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {

    [Header("Botones")]
    public GameObject btHab;
    public GameObject btReturn;
    public GameObject btStart;
    public GameObject btOption;
    public GameObject btLoot;
    public GameObject btItemsConfig;
    public GameObject btHabItems;
    public GameObject btWeap;
    public GameObject btItemsWeap;

    [Header("Paneles")]
    public GameObject gridPane;
    public GameObject gridPaneItems;
    public GameObject gridPaneWeapons;

    public GameObject contenedorPaItemsPlayer;
    public GameObject paItemsSingle;
    public GameObject paWeaponsSingle;
    public GameObject paWeaponAll;

    [Header("Camara")]
    public GameObject camera;

   // private HashSet<int, ItemTemplate> hashItems = new HashSet<int, ItemTemplate>();
    
    private List<GameObject> btHabilidades;


    private float sizeGridPane;

    private GameObject paHabilidades;
    PantallaInicioScript inicio;

    // Use this for initialization
    void Start () {

        inicio = GetComponent<PantallaInicioScript>();

        btHabilidades = new List<GameObject>(GameObject.FindGameObjectsWithTag("Habilidad"));
 
        paHabilidades = GameObject.Find("paHabilidades");
      
        addListeners();

        initButtons();

    }

    void addListeners()
    {
        btHab.GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
        btReturn.GetComponentInChildren<Button>().onClick.AddListener(returnToInicial);
        btStart.GetComponentInChildren<Button>().onClick.AddListener(startGame);
        btOption.GetComponentInChildren<Button>().onClick.AddListener(optionListener);
        btLoot.GetComponentInChildren<Button>().onClick.AddListener(lootListener);
        btItemsConfig.GetComponentInChildren<Button>().onClick.AddListener(itemsConfig);

        btHabItems.GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
        btWeap.GetComponentInChildren<Button>().onClick.AddListener(weaponList);
        btItemsWeap.GetComponentInChildren<Button>().onClick.AddListener(itemsConfig);
    }

    void desactivateAll()
    {
        btHabilidades.ForEach(h => h.SetActive(false));
        btReturn.SetActive(false);
        btHab.SetActive(false);
        btStart.SetActive(false);
        paHabilidades.SetActive(false);
        btItemsConfig.SetActive(false);
        contenedorPaItemsPlayer.gameObject.SetActive(false);
        paItemsSingle.gameObject.SetActive(false);
        btHabItems.SetActive(false);
        btWeap.SetActive(false);
        btItemsWeap.SetActive(false);
        paWeaponsSingle.SetActive(false);
        paWeaponAll.SetActive(false);
    }

    void initButtons()
    {
        desactivateAll();
        btHab.SetActive(true);
        btStart.SetActive(true);
       

    }
    // Update is called once per frame
    void Update () {

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

    void startGame()
    {
        SaveLoad.savePlayer();
        SceneManager.LoadScene("BattleScene");
    }

    void optionListener()
    {
        SaveLoad.clearSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void lootListener()
    {
        SaveLoad.chargeLoot();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void returnToInicial()
    {
        StartCoroutine("delayButtonsVolver");
    }

    void itemsConfig()
    {
        StartCoroutine("delayItemsConfig");
    }

    void habButtonOnClick()
    {
        StartCoroutine("delayButtonsHabilidades");
    }

    // panel de habilidades|||||
    public List<HabilidadTemplate> buildPanelHabilidades(List<HabilidadTemplate> habilidades)
    {
        GameObject single = Resources.Load("Prefabs/UI/btSingle") as GameObject;
        //Ordena los botones alfabeticamente
        habilidades.Sort((x, y) => x.nombre.CompareTo(y.nombre));
        if (habilidades != null)
        {
            foreach (HabilidadTemplate hab in habilidades)
            {
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(
                    gridPane.transform);

                singleClone.transform.localPosition = new Vector3(0, 0, 0);

                singleClone.GetComponent<PaHabScript>().inicializar(hab.nombre, hab.damage, hab.cooldown, hab.maxProximity, hab);
            }
        }

        return habilidades;
    }

    public List<ItemTemplate> buildPanelItems(List<ItemTemplate> items)
    {
        GameObject single = Resources.Load("Prefabs/UI/btSingleItem") as GameObject;
        //Ordena los botones alfabeticamente
        //items.Sort((x, y) => x.nombre.CompareTo(y.nombre));
        if (items != null)
        {
            foreach (ItemTemplate item in items)
            {
                item.itemMaestro = UtilsItems.getItemMaestro(item.identificador);
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(
                    gridPaneItems.transform);

                singleClone.transform.localPosition = new Vector3(0, 0, 0);

                singleClone.GetComponent<ItemSingle>().setItemTemplate(item);
            }
        }

        return items;
    }

    public List<WeaponTemplate> buildPanelWeapon(List<WeaponTemplate> weapons)
    {
        GameObject single = Resources.Load("Prefabs/UI/btSingleWeap") as GameObject;
        //Ordena los botones alfabeticamente
        //items.Sort((x, y) => x.nombre.CompareTo(y.nombre));
        if (weapons != null)
        {
            foreach (WeaponTemplate weap in weapons)
            {
                weap.weaponMaestro = UtilsItems.getWeaponMaestro(weap.identificador);
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(
                    gridPaneWeapons.transform);

                singleClone.transform.localPosition = new Vector3(0, 0, 0);
                singleClone.AddComponent<WeaponSingle>();
                singleClone.GetComponent<WeaponSingle>().setWeaponTemplate(weap);

            }
        }

        return weapons;
    }

    IEnumerator delayButtonsHabilidades()
    {
        desactivateAll();
       

        camera.GetComponent<CamaraInicioScript>().goToHabilidades();

        //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
        yield return new WaitForSeconds(1f);

        btHabilidades.ForEach(h => h.SetActive(true));
        btReturn.SetActive(true);

        List<HabilidadTemplate> habSeleccionadas = SaveLoad.getPlayerTemp().getHabilidadesConjunto1();

        habSeleccionadas.ForEach(h => {

            btHabilidades.ForEach(btHab =>
            {
                if (btHab.GetComponent<BtHabInicioScript>().prioridad == h.prioridad)
                {
                    btHab.GetComponent<BtHabInicioScript>().pintarHabilidadSeleccionada(h);
                }
            });

        });
        btItemsConfig.SetActive(true);

    }

    IEnumerator delayButtonsVolver()
    {
        desactivateAll();
        btHabilidades.ForEach(h => h.GetComponentInChildren<BtHabInicioScript>().limpiarBtLimpiar());

        initButtons();
     
        camera.GetComponent<CamaraInicioScript>().goToInicio();

        yield return new WaitForSeconds(1.2f);
        btHab.SetActive(true);
        btStart.SetActive(true);

    }

    IEnumerator delayItemsConfig()
    {
        desactivateAll();
        btHabilidades.ForEach(h => h.GetComponentInChildren<BtHabInicioScript>().limpiarBtLimpiar());

        camera.GetComponent<CamaraInicioScript>().goToItems();

        yield return new WaitForSeconds(1.2f);
        //btHab.SetActive(true);
        //btStart.SetActive(true);

        contenedorPaItemsPlayer.gameObject.SetActive(true);
        paItemsSingle.gameObject.SetActive(true);

        btHabItems.SetActive(true);
        btWeap.SetActive(true);

    }

    private void weaponList()
    {
        desactivateAll();
        camera.GetComponent<CamaraInicioScript>().goToArmas();
        btItemsWeap.SetActive(true);
        paWeaponsSingle.SetActive(true);
        paWeaponAll.SetActive(true);
    }

}
