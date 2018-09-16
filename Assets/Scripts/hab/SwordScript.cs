using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : WeaponScript
{
  
    public int weapondForce = 5;
    protected PlayerScript owner;
    protected bool functional = true;

    // Use this for initialization
    public new void Start()
    {
        //Ejecuta el Start() del padre
        base.Start();

        setFunctional(true);
        owner = GetComponentInParent<PlayerScript>();

        //Le seteamos a la habilidad el arma que tenga el jugador equipada.
        GetComponentInChildren<SpriteRenderer>().sprite = owner.getWeaponSprite();
    }

    // Update is called once per frame
    public void Update()
    {
        base.utilCalculateCooldown();
    }

    public override void doAction()
    {
        owner.getPlayerController().dashDefault(3.5f);
       // owner.GetComponent<Rigidbody2D>().AddForce(transform.forward * 7);

        functional = true;
        base.doAction();
        owner.enableAnimationRightHand(false);
        GetComponent<Animator>().SetTrigger("swordHit");
        
        //Ejecuta el doAction() del padre para actualizar la variable "nextAction"
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Únicamente afecta a la física de los jugadores
        if (functional && owner.getEnemy() && owner.getEnemy().tag == other.tag)
        {         
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();
            playerScript.tocado(weapondForce, damage);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            functional = false;
        }
        else if (other.tag == "shield")
        {
           if(other.GetComponentInParent<PlayerScript>().tag != owner.tag)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                functional = false;
                owner.tocado(weapondForce, 0);
            }

            other.GetComponent<ShieldScript>().tocado();
        }
    }
    
}
