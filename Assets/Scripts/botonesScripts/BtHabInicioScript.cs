using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtHabInicioScript : MonoBehaviour, IDropHandler {

    public int prioridad;
    public Text nombre;
    public Text damage;
    public Text CC;
    public Text prox;
    public GameObject btLimpiar;
    private GameObject paHabilidades;
    
    private HabilidadTemplate habSeleccionada;

    // Use this for initialization
    void Awake ()
    {
        paHabilidades = GameObject.Find("paHabilidades");

        GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
        btLimpiar.GetComponentInChildren<Button>().onClick.AddListener(btLimpiarListener);
        limpiarBtLimpiar();
    }

    private void habButtonOnClick()
    {
           
        paHabilidades.SetActive(true);
    }

    public void pintarHabilidadSeleccionada(HabilidadTemplate hab)
    {
        nombre.text = hab.nombre;
        damage.text = hab.damage.ToString();
        CC.text = hab.cooldown.ToString();
        prox.text = hab.maxProximity.ToString();
        hab.prioridad = prioridad;
        habSeleccionada = hab;
    }

    public HabilidadTemplate getHabilidad()
    {
        return habSeleccionada;
    }


    public void OnDrop(PointerEventData eventData)
    {
        HabilidadTemplate habTemplate = eventData.pointerDrag.gameObject.GetComponentInChildren<PaHabScript>().getHabilidad();
        List<GameObject> habilidades = new List<GameObject>(GameObject.FindGameObjectsWithTag("Habilidad"));
        habilidades.ForEach(h =>
        {
            if (habTemplate == h.GetComponentInChildren<BtHabInicioScript>().getHabilidad()){
                if (habSeleccionada != null){
                    h.GetComponentInChildren<BtHabInicioScript>().pintarHabilidadSeleccionada(habSeleccionada);
                }
                else {
                    h.GetComponentInChildren<BtHabInicioScript>().limpiarBtLimpiar();
                }
            }
        });

        pintarHabilidadSeleccionada(habTemplate);
        SaveLoad.saveHabilidadConjunto1(habTemplate);
    }

    private void btLimpiarListener()
    {
        SaveLoad.removeHabilidadConjunto1(habSeleccionada);
        limpiarBtLimpiar();
    }

    public void limpiarBtLimpiar()
    {
        nombre.text = "Vacio";
        damage.text = "";
        CC.text = "";
        prox.text = "";
        habSeleccionada = null;
    }
}
