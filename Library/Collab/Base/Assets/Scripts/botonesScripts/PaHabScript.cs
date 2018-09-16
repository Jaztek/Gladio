using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PaHabScript : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler, IDropHandler
{

    public Vector3 startPosition;
    static Transform transformParent;
    public static HabilidadTemplate habilityBeingDragged;

    public Text nombre;
    public Text damage;
    public Text CC;
    public Text prox;

    private HabilidadTemplate habilidadSingle; 
    private GameObject canvas;


    private void Start()
    {
       canvas =GameObject.Find("Canvas");
       GetComponentInChildren<Button>().onClick.AddListener(singleOnClick);  

    }

    public void inicializar(string nombreHab, float damageHab, float CCHab, float proxHab, HabilidadTemplate habilidad)
    {
        print(nombreHab);
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

    void singleOnClick()
    {
       // butonPadre.pintarHabilidadSeleccionada(habilidadSingle);
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        habilityBeingDragged = habilidadSingle;
        transformParent = transform.parent;
        transform.SetParent(canvas.transform);
        startPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        habilityBeingDragged = null;
        transform.position = startPosition;
        transform.SetParent(transformParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        print("ondrop");
       // pintarHabilidadSeleccionada(PaHabScript.habilityBeingDragged);
    }
}
