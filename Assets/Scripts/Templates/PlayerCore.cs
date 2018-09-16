using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;

[System.Serializable]
public class PlayerCore{


    public ObjectId Id { get; set; }

    public string name;

    public List<HabilidadTemplate> habilidades = new List<HabilidadTemplate>();

    public List<ItemTemplate> equip = new List<ItemTemplate>();

    public WeaponTemplate weapon = null;

    public int level;

}
