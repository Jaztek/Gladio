using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsWeapon {

   private static Dictionary<int, string> mapWeapon = new Dictionary<int, string>();

    // Use this for initialization

    public static string getWeaponHab(int id)
    {
        string value = null;
        if (getMap().ContainsKey(id))
        {
            value = mapWeapon[id];
        }
        return value;
    }

    public static Dictionary<int, string> getMap()
    {
        if (mapWeapon.Count == 0 )
        {
            mapWeapon.Add(1, "habEspadazo");
            mapWeapon.Add(2, "habEstocada");
        }

        return mapWeapon;
    }


	
	
}
