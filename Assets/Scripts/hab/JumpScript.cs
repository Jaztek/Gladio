using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : WeaponScript
{
   
    public int jumpForce= 10;
   

    // Use this for initialization
    new void Start()
    {
        //Ejecuta el Start() del padre
        base.Start();

        setFunctional(true);
    }

    // Update is called once per frame
    public void Update()
    {
        base.utilCalculateCooldown();
    }

    public override void doAction()
    {
        getPlayer().doJump(jumpForce);
        //Ejecuta el doAction() del padre para actualizar la variable "nextAction"
        base.doAction();
    }
}
