using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {


    public GameObject textoFin;

    private void Awake()
    {
        //Creamos el player
        Vector3 posicionPlayer = new Vector3(-7f, -2.5f, 0f);
        GameObject player = Instantiate(SaveLoad.Load(), posicionPlayer, new Quaternion());

        //Creamos el enemy
        GameObject enemy = QueryMaster.findRandomEnemy();

        //Gestionamos los botones y asignamos el enemigo a ambos
        player.GetComponent<PlayerController>().setFunctional(true);
        player.GetComponent<PlayerController>().enemy = enemy;
        enemy.GetComponent<PlayerController>().enemy = player;
        gestionarBotones(player);

        GameObject habButton = GameObject.Find("btVolver");
        habButton.GetComponentInChildren<Button>().onClick.AddListener(habVolverOnClick);
    }

    private void Update()
    {
        gestionPrioridadesHabilidades();
    }

    public bool gestionPrioridadesHabilidades()
    {
       
        bool valida = false;

        List<WeaponScript> weapons = new List<WeaponScript>();

        weapons.AddRange(UtilsHabilidades.getWeaponGestion());

        foreach (WeaponScript weap in weapons)
        {
            UtilsHabilidades.removHab(weap);
            valida = decalajeHabilidades(weap.getIsPlayer(), weap);
            weap.callDoAction(valida);
        }
        return false;
    }

    private void gestionarBotones(GameObject player)
    {
        //Añadimos las habilidades del player
        List<ButtonScript> buttons = new List<ButtonScript>(GameObject.FindObjectsOfType<ButtonScript>().Where(b => b.tag == "Habilidad"));
        //Ordena los botones alfabeticamente
        buttons.Sort((x, y) => x.name.CompareTo(y.name));

        // Setea los botones a las habilidades
        UtilsPlayer.habilitiesPlayer = new List<WeaponInterface>(player.transform.GetComponentsInChildren<WeaponInterface>());
        UtilsPlayer.habilitiesPlayer.Sort((x, y) => ((WeaponScript)x).prioridad.CompareTo(((WeaponScript)y).prioridad));
        UtilsPlayer.habilitiesPlayer.ForEach(h => h.setButtonScript(buttons.ElementAt(UtilsPlayer.habilitiesPlayer.IndexOf(h))));
    }

    void habVolverOnClick()
    {
       SceneManager.LoadScene("PantallaInicio");
    }

    public bool decalajeHabilidades(bool isPlayer, WeaponScript scriptWeapon)
    {
        bool validoActivarse = false;
        if (isPlayer)
        {
            foreach (WeaponScript h in UtilsPlayer.habilitiesPlayer) { 
                if (h != scriptWeapon)
                {
                    validoActivarse = h.gestionarHabPrioridad(scriptWeapon.prioridad);
                    if (!validoActivarse)
                    {
                        break;
                    }
                     h.decalajeHabilidad();
                }
              
            }
           
        }
        else{
            foreach (WeaponScript h in UtilsPlayer.habilitiesEnemy)
            {
                if (h != scriptWeapon)
                {
                    validoActivarse = h.gestionarHabPrioridad(scriptWeapon.prioridad);
                    if (!validoActivarse)
                    {
                        break;
                    }
                    h.decalajeHabilidad();
                }
            }
        }
        return validoActivarse; 
    }

    public void dead(string texto)
    {
        textoFin.GetComponent<Text>().text = texto;

        StartCoroutine("delayDead");
    }

    IEnumerator delayDead()
    {
         
        GameObject textoClone = Instantiate(textoFin);

        textoClone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);


        textoFin.transform.localScale = new Vector3(2, 2);
        textoClone.transform.localPosition = new Vector3(0, 0);

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("PantallaInicio");


    }
}
