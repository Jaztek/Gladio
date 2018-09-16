using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArrowScript : WeaponScript
{

   
    public int weapondForce = 3;
    public GameObject shot;

    // Use this for initialization
    new void Start()
    {
        //Ejecuta el Start() del padre
        base.Start();
        
        setFunctional(true);
    }

    // Update is called once per frame
    void Update()
    {
        base.utilCalculateCooldown();
    }

    public override void doAction()
    {     
        //Lanza siempre la flecha hacia delante. 
        GameObject shotClone = Instantiate(shot, transform.position, new Quaternion());
        shotClone.GetComponent<ArrowScript>().setOwner(getPlayer());
        shotClone.GetComponent<ArrowScript>().setDamage(getDamage());
        
        base.doAction();
    }

}
