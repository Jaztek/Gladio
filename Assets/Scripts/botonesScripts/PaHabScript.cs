using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaHabScript : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler, IDropHandler
{
    public Text nombre;
    public Text damage;
    public Text CC;
    public Text prox;

    private HabilidadTemplate habilidadSingle; 
    private GameObject canvas;
    public GameObject inicio;


    private void Start()
    {
       canvas = GameObject.Find("Canvas");
       inicio = GameObject.Find("ConfigControllerScript");
    }

    public void inicializar(string nombreHab, float damageHab, float CCHab, float proxHab, HabilidadTemplate habilidad)
    {
        nombre.text = nombreHab;
        damage.text = damageHab.ToString();
        CC.text = CCHab.ToString();
        prox.text = proxHab.ToString();
        habilidadSingle = habilidad;
      
    }

    public HabilidadTemplate getHabilidad()
    {
        return habilidadSingle;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject gridPane = GameObject.Find("gridPane");
        foreach (Transform child in gridPane.transform)
        {
            if (!child.Equals(transform))
            {
               GameObject.Destroy(child.gameObject);
            }
        }

        inicio.GetComponent<PantallaInicioScript>().buildPanelHabilidades();

        transform.SetParent(canvas.transform);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Object.Destroy(this.gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Necesita estar implementado
    }
}
