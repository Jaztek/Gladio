using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public int speed = 10;
    public float damage = 10;
    private Rigidbody2D rigidbody;
    private PlayerScript owner;

    void Start()
    {
        //seteamos el rigidbody para mover 
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movemos la daga en cada update. No cae*
        rigidbody.velocity = new Vector2(owner.getPlayerController().isFacingRight() * speed, rigidbody.velocity.y);
        rigidbody.AddForce(transform.forward * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //si el colisionador es el enemigo del owner, hace el tocado()
        if (owner.getEnemy().tag ==  other.tag)
        {
            other.GetComponent<PlayerScript>().tocado(2, damage);

            //destruye la flecha
            Destroy(this.gameObject);
        }
        else if (other.tag == "shield")
        {
           if(other.GetComponentInParent<PlayerScript>().tag != owner.tag)
            {

                print("ESCUDADO");
                Destroy(this.gameObject);
            }
        }
       
    }

    //seteamos el PlayerScript que ha lanzado la flecha.
    public void setOwner(PlayerScript arrowowner)
    {
        owner = arrowowner;
    }

    public void setDamage(float damageWeap)
    {
        damage = damageWeap;
    }
}
