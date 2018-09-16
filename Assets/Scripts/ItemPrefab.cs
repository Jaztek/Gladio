using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MongoDB.Bson;

[System.Serializable]
public class ItemMaestro {

    public ObjectId Id { get; set; }
    public string identificador;
    public string sprite;
    public int portentaje; 
    public int posicion;

}
