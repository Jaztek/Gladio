using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : WeaponScript
{
   
    public float shieldDuration = 3f;

    private float nextAction = 0f;
    private bool shieldActive = false;
    private PlayerScript owner;
    Animator anim;

    // Use this for initialization
    new void Start()
    {
        //Ejecuta el Start() del padre
        base.Start();

        setFunctional(true);

        owner = GetComponentInParent<PlayerScript>();
        //Le seteamos a la habilidad el arma que tenga el jugador equipada.
        GetComponent<SpriteRenderer>().sprite = owner.getWeaponLeftSprite();
        anim = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        base.utilCalculateCooldown();

        if (shieldActive)
        {
            nextAction -= Time.deltaTime;
            if (nextAction <= 0)
            {    
                enableAction(false);
              
            }


        }
    }

    public override void doAction()
    {
        //Ejecuta el doAction() del padre para actualizar la variable "nextAction"
        base.doAction();

        nextAction = shieldDuration;
        enableAction(true);

    }

    private void enableAction(bool enable)
    {
        shieldActive = enable;
        GetComponent<Collider2D>().enabled = enable;
        anim.SetBool("shieldStand", enabled);

        //esto deberia ir con un 
        //anim.SetBool("shieldStand", enable);
        // pero oh magia, no lo hace.
        // el true si que lo hace bien.

        if (!enable)
        {
            anim.SetBool("shieldStand", false);
        }
    }

    public void tocado()
    {
        enableAction(false);
    }
}
