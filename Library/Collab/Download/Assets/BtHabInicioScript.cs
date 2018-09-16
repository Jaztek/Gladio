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
    private GameObject gridPane;
    private float sizeGridPane;

    private HabilidadTemplate habSeleccionada;

    // Use this for initialization
    void Awake ()
    {
        paHabilidades = GameObject.Find("paHabilidades");
        gridPane = GameObject.Find("gridPane");
        GetComponentInChildren<Button>().onClick.AddListener(habButtonOnClick);
    }
	
	// Update is called once per frame
	void Update () {
        float posicionYGridPane = gridPane.GetComponentInChildren<RectTransform>().anchoredPosition.y;
        float posicionXGridPane = gridPane.GetComponentInChildren<RectTransform>().anchoredPosition.x;
        if (gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y < 300) {
            sizeGridPane = gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y;
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, (300 - sizeGridPane) / 2);
        } else if (gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y != sizeGridPane) {
          sizeGridPane = gridPane.GetComponentInChildren<RectTransform>().sizeDelta.y;
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, -(sizeGridPane - 300) / 2);
        } else if (posicionYGridPane < -((sizeGridPane - 300) / 2)) {
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, -(sizeGridPane - 300) / 2);
        } else if (posicionYGridPane > ((sizeGridPane - 300) / 2)) {
            gridPane.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(posicionXGridPane, (sizeGridPane - 300) / 2);
        }
    }

    private void habButtonOnClick()
    {
        paHabilidades.SetActive(true);

        GameObject single = Resources.Load("Prefabs/UI/btSingle") as GameObject;

        List<HabilidadTemplate> habilidades = SaveLoad.getHabilities();
        if ( habilidades!=null )
        {
            foreach (HabilidadTemplate hab in habilidades)
            {
                GameObject singleClone = Instantiate(single);
                singleClone.transform.SetParent(gridPane.transform);

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
}
