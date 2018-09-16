using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsItems : MonoBehaviour {

    static List<ItemMaestro> items;
    static List<WeaponMaestro> weapons;
    
   // Use this for initialization
   void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static List<ItemMaestro> getMaestroItems()
    {
        if (items == null)
        {
            items = QueryMaster.getMaestroItems();          
        }
        return items;
    }


    public static ItemMaestro getItemMaestro(string id)
    {
        foreach(ItemMaestro item in getMaestroItems())
            {
                if (item.identificador.Equals(id))
                {
                    return item;
                }
            }
            return null;
    }

    public static WeaponMaestro getWeaponMaestro(string id)
    {
        foreach (WeaponMaestro weap in findMaestroWeapon())
        {
            if (weap.identificador.Equals(id))
            {
                return weap;
            }
        }
        return null;
    }


    public static List<WeaponMaestro> findMaestroWeapon()
    {
        if (weapons == null)
        {
            weapons = QueryMaster.getMaestroWeapons();
        }
        return weapons;
    }

    public static Sprite getSprite(string itemSprite)
    {
       return Resources.Load("Sprites/Raminstudios/" + itemSprite , typeof(Sprite)) as Sprite;
    }

    public static Sprite getSpriteWeapon(string itemSprite)
    {
      
        return Resources.Load("Sprites/Raminstudios/Armas/" + itemSprite, typeof(Sprite)) as Sprite;
    }
}
