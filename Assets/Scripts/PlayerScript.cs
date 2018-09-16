using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour {

    public int maxLife = 10;
    public int actLife;
    public float powerStun = 2f;
    public float reduccionDaño = 1;
    public GameObject rightHand;
    public Sprite weaponRightSprite;
    public Sprite weaponLeftSprite;

    private PlayerController playerController;
    private new Rigidbody2D rigidbody;
    private Animator playerAnim;
    private bool freezeCD;
    private bool isDanyandose = false;

    public GameObject headGO;
    public GameObject troncoGO;
    public GameObject piesGO;
    public GameObject rightHandGO;

    private ItemTemplate temHead;
    private ItemTemplate temTronco;
    private ItemTemplate temPies;

    private WeaponTemplate weaponTemplate;
    private bool isDead = false;


    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerAnim = GetComponent<Animator>();

        actLife = maxLife;
        if (GameObject.Find("vidaPlayer"))
        {
            GameObject.Find("vidaPlayer").GetComponentInChildren<Slider>().value = 1;
            GameObject.Find("vidaEnemy").GetComponentInChildren<Slider>().value = 1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerController.getFunctional())
        {
            checkDamage();
        }
    }

   
    public void tocado(int force, float damage)
    {
        playerController.setCanMove(false);
        playerController.applyForce(force);
     
        doDamage(damage);
        
    }

    public void doJump(int force)
    {
        playerController.doJump(force);
    }

    public void doDamage(float damage)
    {
        if (!isDanyandose)
        {
            actLife = actLife - (int)(damage * reduccionDaño);
            if(reduccionDaño < 1)
            {
                Debug.Log("Has recibido " + (damage * reduccionDaño) + " -> " + damage + "(-" + (damage * reduccionDaño) + ")");
            }
            //llamo a la corrutina que hace parpadear al personaje.
            StartCoroutine("changeColorCorrutine");
        }
    }
    public void checkDamage()
    {
        if(actLife <= 0 && !isDead)
        {
            dead();
        }

        if (tag == "Player")
        {
            GameObject.Find("vidaPlayer").GetComponentInChildren<Slider>().value = (float)actLife / (float)maxLife;
        }
        else if (tag == "Enemy")
        {
            GameObject.Find("vidaEnemy").GetComponentInChildren<Slider>().value = (float)actLife / (float)maxLife;
        }
    }

    private void dead()
    {
        isDead = true;
        piesGO.GetComponent<Animator>().SetTrigger("dead");
        headGO.GetComponent<Animator>().SetTrigger("dead");
        troncoGO.GetComponent<Animator>().SetTrigger("dead");
        rightHandGO.GetComponent<Animator>().SetTrigger("dead");

        playerController.setCanMove(false);

        String mensajeDead;
        if (tag == "Player")
        {
            mensajeDead = "Mal pringao";
        }
        else
        {
            mensajeDead = "You win this time";
        }

        if (GameObject.FindGameObjectWithTag("GameController"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().dead(mensajeDead);
        }

    }

    public void stuned(float powerStun)
    {
        playerAnim.SetBool("stuned", true);
        playerController.setCanMove(false);
        this.powerStun = Time.time + powerStun;
    }

    public PlayerController getPlayerController()
    {
        return playerController;
    }

    public float getPowerStun()
    {
        return powerStun;
    }

    public bool canMove()
    {
        return playerController.getCanMove();
    }

    public GameObject getEnemy()
    {
        return playerController.getEnemy();
    }
    public Sprite getWeaponSprite()
    {
        return weaponRightSprite;
    }
    public Sprite getWeaponLeftSprite()
    {
        return weaponLeftSprite;
    }

    public void setFreezeCD()
    {
        freezeCD = true;
    }

    public int getVidaMaxima()
    {
        return maxLife;
    }

    public int getVidaActual()
    {
        return actLife;
    }

    IEnumerator changeColorCorrutine()
    {
        //Prueba de corrutinas, el personaje parpadea cuando es dañado
        // Las corrutinas son como "hilos" que se ejecutan a parte y cuando se les dice (no por cada frame) lo
        // que mejora el rendimiento.
        isDanyandose = true;
        playerController.delayMove = true;
       

        //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
        yield return new WaitForSeconds(.8f);
     
        isDanyandose = false;
        playerController.delayMove = false;

    }

    public void enableAnimationRightHand(bool enable)
    {
        rightHand.GetComponent<SpriteRenderer>().enabled = enable;
    }

    public void aplicarReduccionDaño(float reduccion)
    {
        //0 es inmunidad y 1 es recibir todo el daño
        if(reduccion < 0)
        {
            reduccionDaño /= -reduccion;
        }
        else
        {
            reduccionDaño *= reduccion;
        }
    }

    public void setSpriteTo(ItemTemplate item)
    {
        int zone = item.itemMaestro.posicion;
        switch (zone)
        {
             case 1:      
                headGO.GetComponent<SpriteRenderer>().sprite =
                    UtilsItems.getSprite(item.itemMaestro.sprite);
                temHead = item;
                break;
            case 2:
                troncoGO.GetComponent<SpriteRenderer>().sprite = UtilsItems.getSprite(item.itemMaestro.sprite);
                temTronco = item;
                break;
            case 3:
                List<SpriteRenderer> spritesRend = new List<SpriteRenderer>(piesGO.GetComponentsInChildren<SpriteRenderer>());
                spritesRend.ForEach(h => h.sprite = UtilsItems.getSprite(item.itemMaestro.sprite));
                temPies = item;
                break;
        }
    }

    public void setWeaponTo(WeaponTemplate weap)
    {
        weaponTemplate = weap;
        weaponRightSprite = UtilsItems.getSpriteWeapon(weap.weaponMaestro.sprite);
        rightHandGO.GetComponent<SpriteRenderer>().sprite = weaponRightSprite;
    }

    public void setMoviendoseAnim(bool isMoviendose)
    {
        piesGO.GetComponent<Animator>().SetBool("isMoving", isMoviendose);
    }
}
