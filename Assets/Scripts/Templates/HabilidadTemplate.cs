using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HabilidadTemplate {

    public string nombre {  get; set; }
    public float damage { get; set; }
    public int curacion { get; set; }
    public float maxProximity { get; set; }
    public float cooldown {  get; set; }
    public int prioridad { get; set; }
    public int equipada = 1;

}
