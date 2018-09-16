using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSingle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public Sprite sprite;
    private ItemTemplate item;
    private GameObject canvas;
    private GameObject inicio;

    // 1 = cabeza, 2 = tronco, 3. pieces.
    private int posicion;
    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas");
        inicio = GameObject.Find("ConfigControllerScript");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public ItemTemplate getItem()
    {
        return item;
    }
    public void setItemTemplate(ItemTemplate itemTemp)
    {
         item = itemTemp;
        //item.itemPrefab.sprite;

        // Sprite myFruit = Resources.Load("fruits_1", typeof(Sprite)) as Sprite;
        if (item.itemMaestro!= null)
        { 
            sprite = UtilsItems.getSprite(item.itemMaestro.sprite); 
            GetComponent<Image>().sprite = sprite;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject gridPane = GameObject.Find("gridPaneItems");
        foreach (Transform child in gridPane.transform)
        {
            if (!child.Equals(transform))
            {
                //GameObject.Destroy(child.gameObject);
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
