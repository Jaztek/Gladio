using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasosSombrasScript : SwordScript
{

    // Use this for initialization
    new void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }

    public override void doAction()
    {
        GameObject enemigo = GameObject.FindGameObjectWithTag("Enemy");
        Vector3 posicionEnemy = enemigo.transform.position;
        //Salto detras del enemigo y se da la vuelta
        owner.transform.position = new Vector3(posicionEnemy.x + (2.2f * -enemigo.GetComponentInChildren<PlayerController>().isFacingRight()), posicionEnemy.y, posicionEnemy.z);
        owner.transform.localScale = new Vector3(enemigo.transform.localScale.x, owner.transform.localScale.y, owner.transform.localScale.z);
        base.doAction();
    }
}
 