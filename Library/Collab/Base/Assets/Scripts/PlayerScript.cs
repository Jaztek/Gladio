using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private new Rigidbody2D rigidbody;
    public int speed = 5;
    public GameObject enemy;

    public bool canMove = true;
    public float timeStun = 0f;
    private float nextJump = 0f;
    public float fireRate;
    int IsFacingRight = 1;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        move();

    }


    private void move()
    {
        if(Time.time > timeStun)
        {
            canMove = true;
        }
        //Si puede moverse y mi siguiente movimiento no es un salto.
        if (canMove && Time.time > nextJump)
        {
            //Retraso el salto.
            nextJump = Time.time + fireRate;
            
            //Calculo hacia que dirección debe ir el jugador.
            float absPosition = enemy.transform.position.x - this.transform.position.x;
            int IsFacingRightNow = (absPosition < 0) ? -1 : 1;

            //Aplica la velocidad al jugador.
            rigidbody.velocity = new Vector2(IsFacingRightNow * speed, rigidbody.velocity.y); 
            rigidbody.AddForce(transform.forward * speed);

            //En caso de que el enemigo este detras tuya se da la vuelta.
            if (!IsFacingRightNow.Equals(IsFacingRight))
            {
                IsFacingRight = IsFacingRightNow;
                //Se hace negativa la x para invertir la dirección del jugador.
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      
    }
    void OnTriggerStay(Collider collider)
    {
      
    }

    public void tocado(int force)
    {
        //Se calcula teniendo en cuenta tu dirección para hacerlo genérico.
        rigidbody.velocity = new Vector2(-IsFacingRight * force, rigidbody.velocity.y);
        rigidbody.AddForce(transform.forward * force);
    }

       
    public void doJump(int force)
    {
        //Se calcula teniendo en cuenta tu dirección para hacerlo genérico.
        rigidbody.velocity = new Vector2(IsFacingRight * speed, force);
        rigidbody.AddForce(transform.forward * force);
    }
}
