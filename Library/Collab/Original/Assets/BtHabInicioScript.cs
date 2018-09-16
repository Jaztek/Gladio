using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtHabInicioScript : MonoBehaviour {

    public Text nombre;
    public Text damage;
    public Text CC;
    public Text prox;
    private GameObject paHabilidades;

    private HabilidadTemplate habSeleccionada;

    // Use this for initialization
    void Awake ()
    {
        paHabilidades = GameObject.Find("paHabilidades");
        GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void habButtonOnClick()
    {
        paHabilidades.SetActive(true);

        GameObject single = Resources.Load("Prefabs/UI/btSingle") as GameObject;

        List<HabilidadTemplate> habilidades = SaveLoad.getHabilities();
        if (habilidades!=null)
        {
            foreach (HabilidadTemplate hab in habilidades)
            {
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(paHabilidades.transform);
              
                singleClone.transform.localPosition = new Vector3(0, 0, 0);

                singleClone.GetComponent<PaHabScript>().inicializar(hab.nombre, hab.damage, hab.cooldown,hab.maxProximity, hab, this);
            }
        }
    }

    public void pintarHabilidadSeleccionada(HabilidadTemplate hab)
    {
        paHabilidades.SetActive(false);

        nombre.text = hab.nombre;
        damage.text = hab.damage.ToString();
        CC.text = hab.cooldown.ToString();
        prox.text = hab.maxProximity.ToString();
        habSeleccionada = hab;
    }

    public HabilidadTemplate getHabilidad()
    {
        return habSeleccionada;
    }
}
