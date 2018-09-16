using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPlayerPanel : MonoBehaviour, IDropHandler
{
    private PlayerScript player;

    private ItemTemplate itemTemp;
	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrop(PointerEventData eventData)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        ItemTemplate itemTemplate = eventData.pointerDrag.gameObject.GetComponentInChildren<ItemSingle>().getItem();
        player.setSpriteTo(itemTemplate);
        itemTemp = itemTemplate;

        SaveLoad.saveItemsConjunto1(itemTemp);
    }

    public ItemTemplate getItemTemplate()
    {
        return itemTemp;
    }
}
