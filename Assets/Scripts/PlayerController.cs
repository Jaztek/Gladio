using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public LayerMask groundMask;
    public GameObject enemy;
    public int speed = 5;
    private bool vueltaReady = true;
    PlayerScript playerScript;

    private float nextJump = 0f;
    public float fireRate;

    private new Rigidbody2D rigidbody;

    public float alcanceAtaque =3f;

    [Header("FlagsMovimiento")]
    private bool canMove = true;
    private bool functional = true;
    public bool delayMove = false;

    // Use this for initialization
    void Start () {
        delayMove = false;
        alcanceAtaque = 4f;
        rigidbody = GetComponent<Rigidbody2D>();

        playerScript = GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (!functional)
        {
            // entrara por aqui cuando queramos que el player sea florero, como en la configuración.
            return;
        }
        isGrounded();

        move();

    }

    private void isGrounded()
    {
        Vector3 dir = transform.TransformDirection(Vector3.up) * -3;
        Debug.DrawRay(transform.position, dir, Color.red);

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, dir, 2f, groundMask);
        if (raycastHit && raycastHit.collider && raycastHit.collider.CompareTag("Ground"))
        {
            canMove = true;
        }
        else
        {
            canMove = false;

        }
    }

    private void move()
    {
        //Si puede moverse y mi siguiente movimiento no es un salto.
        if (getCanMove() && Time.time > nextJump)
        {
            //Retraso el salto.
            nextJump = Time.time + fireRate;

            //Calculo hacia que dirección debe ir el jugador.
            float posicionEnemigo = enemy.transform.position.x - this.transform.position.x;
            int isEnemigoDelante = (posicionEnemigo < 0) ? -1 : 1;
            float distancia = Mathf.Abs(posicionEnemigo);
            //Aplica la velocidad al jugador.
            if (distancia > alcanceAtaque)
            {
                rigidbody.velocity = new Vector2(isEnemigoDelante * speed, rigidbody.velocity.y);
                rigidbody.AddForce(transform.forward * speed);
                playerScript.setMoviendoseAnim(true);
            } else if(distancia + 1 < alcanceAtaque)
            {
                rigidbody.velocity = new Vector2(-isEnemigoDelante * (speed / 4), rigidbody.velocity.y);
                rigidbody.AddForce(transform.forward * (speed / 4));
                playerScript.setMoviendoseAnim(true);
            } else
            {
                playerScript.setMoviendoseAnim(false);
            }

            //En caso de que el enemigo este detras tuya se da la vuelta.
            bool isEnemigoDetras = isEnemigoDelante * isFacingRight() != 1;
            if (isEnemigoDetras && vueltaReady)
            {
                StartCoroutine("delayUnFacing");
            }
        } else
        {
            playerScript.setMoviendoseAnim(false);
        }
    }

    public void applyForce(int force)
    {
        //Se calcula teniendo en cuenta tu dirección para hacerlo genérico.
        rigidbody.velocity = new Vector2(-isFacingRight() * force, rigidbody.velocity.y);
        rigidbody.AddForce(transform.forward * force);
    }

    public void doJump(int force)
    {
        //Se calcula teniendo en cuenta tu dirección para hacerlo genérico.
        rigidbody.velocity = new Vector2(isFacingRight() * speed, force);
        rigidbody.AddForce(transform.forward * force);
    }

    public bool getCanMove()
    {
        return canMove && !delayMove;
    }

    public void setCanMove(bool canMoveParam)
    {
        canMove = canMoveParam;
    }

    public bool getFunctional()
    {
        return functional;
    }

    public void setFunctional(bool functionalParam)
    {
        functional = functionalParam;
    }

    public GameObject getEnemy()
    {
        return enemy;
    }

    public int isFacingRight()
    {
        return transform.localScale.x > 0 ? 1 : -1;
    }

    public void dashDefault(float force)
    {
       rigidbody.velocity = new Vector2(isFacingRight() * force, rigidbody.velocity.y);
    }

    IEnumerator delayUnFacing()
    {

        vueltaReady = false;
        yield return new WaitForSeconds(1.2f);
      
        //Se hace negativa la x para invertir la dirección del jugador.
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        vueltaReady = true;

    }
}
