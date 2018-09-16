using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsHabilidades  {

    public static List<WeaponScript> weapons = new List<WeaponScript>();
  
   
    public static void addHab(WeaponScript hab)
    {
        weapons.Add(hab);
    }

    public static void removHab(WeaponScript hab)
    {
        weapons.Remove(hab);

    }
    public static List<WeaponScript> getWeaponGestion()
    {
        return weapons;
    }
}
