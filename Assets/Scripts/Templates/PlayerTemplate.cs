using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

[System.Serializable]
public class PlayerTemplate
{

  //  public ObjectId Id { get; set; }

    public PlayerCore playerCore;

    public int conjuntoActivo = 1;
    public List<HabilidadTemplate> habilidadesConjunto1 = new List<HabilidadTemplate>();
    public List<ItemTemplate> itemConjunto1 = new List<ItemTemplate>();
    public WeaponTemplate weaponConjunto1 = null;

    public List<HabilidadTemplate> habilidadesConjunto2 = new List<HabilidadTemplate>();
    public List<ItemTemplate> itemConjunto2 = new List<ItemTemplate>();
    public WeaponTemplate weaponConjunto2 = null;

    public List<HabilidadTemplate> habilidadesConjunto3 = new List<HabilidadTemplate>();
    public List<ItemTemplate> itemConjunto3 = new List<ItemTemplate>();
    public WeaponTemplate weaponConjunto3 = null;


    public List<ItemTemplate> items = new List<ItemTemplate>();
    public List<HabilidadTemplate> habilidades = new List<HabilidadTemplate>();
    public List<WeaponTemplate> weapons = new List<WeaponTemplate>();

    public int oro = 0;


    // Gestion de Habilidades:

    public void setHabilidadesConjunto1(List<HabilidadTemplate> listHabilid)
    {
        habilidadesConjunto1 = listHabilid;
    }

    public List<HabilidadTemplate> getHabilidadesConjunto1()
    {
        return habilidadesConjunto1;
    }
    public List<HabilidadTemplate> getHabilidades()
    {
        return habilidades;
    }

    public void addHabilidades(List<HabilidadTemplate> listHabilid)
    {
        habilidades.AddRange(listHabilid);
    }

    // Gestion de Items:

    public void setItemsConjunto1(List<ItemTemplate> items)
    {
        itemConjunto1 = items;
    }

    public List<ItemTemplate> getItemsConjunto1()
    {
        return itemConjunto1;
    }
    public List<ItemTemplate> getItems()
    {
        return items;
    }

    public void addItems(List<ItemTemplate> itemsAdd)
    {
        items.AddRange(itemsAdd);
    }

    // gestion weapon
    public void setWeaponConjunto1(WeaponTemplate weap)
    {
        weaponConjunto1 = weap;
    }

    public WeaponTemplate getWeaponConjunto1()
    {
        return weaponConjunto1;
    }
    public List<WeaponTemplate> getWeapons()
    {
        return weapons;
    }

    public void addWeapons(List<WeaponTemplate> weapAdd)
    {
        weapons.AddRange(weapAdd);
    }


    // Oros

    public int getOro()
    {
        return oro;
    }

    public void setOro(int oro)
    {
        this.oro = oro;
    }
}
