using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PasivaScript : WeaponScript
{
   
    public new void Start () {
        isPasiva = true;
        player = this.transform.GetComponentInParent<PlayerScript>();
    }
}
