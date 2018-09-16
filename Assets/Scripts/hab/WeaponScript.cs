using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaponScript : MonoBehaviour, WeaponInterface
{
    public int prioridad = 0;
    public float coolDown;
    private float nextAction = 0f;
    public float maxProximity;
    public float damage;

    private bool functional = true;
    protected bool ready = true;
    protected Image image;
    protected Text text;
    private GameController gameController;
    protected PlayerScript player;

    protected bool isPasiva = false;
    protected ButtonScript button;

    public int alcance ;

    // Lo llama el hijo cuando se inicializa
    public void Start () {
        ready = true;
        player = this.transform.GetComponentInParent<PlayerScript>();
        nextAction = coolDown;
        if (button && player.CompareTag("Player"))
        {
            image = new List<Image>(button.GetComponentsInChildren<Image>()).Where(i => i.tag == "Cooldown").Single<Image>();
            text = new List<Text>(button.GetComponentsInChildren<Text>()).Where(i => i.tag == "Cooldown").Single<Text>();

            coolDownAzul();

        }
        if (GameObject.FindGameObjectWithTag("GameController"))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

    }


    // Lo llama el hijo cada frame
    public void utilCalculateCooldown()
    {
        if (!player.getPlayerController().getFunctional())
        {
            return;
        }
        nextAction -= Time.deltaTime;
      
        //Sólamente el jugador "Player" tiene botones que gestionar
        if (player.CompareTag("Player"))
        {
            gestionarCoolDown();
        }
        //Añadimos un +1 a nextAction para que el cooldown llegue a 0
        //Únicamente aplicamos la acción si se puede mover 
        if (nextAction <= 0 && player.canMove() && functional && ready)
        {
            doActionWeapon();
        }
    }
 
    private void gestionarCoolDown()
    {
      
        //Una vez calculado el color le asignamos transparencia
        image.fillAmount = 1 - nextAction / getCoolDown();
        if (image.fillAmount == 1)
        {
            //La habilidad esta lista para usarse
            image.fillAmount = 0;
            text.text = "";
        }
        else
        {
            text.text = Math.Ceiling(nextAction).ToString();
        }
    }
    public virtual void doActionWeapon()
    {
        
        if(maxProximity != 0)
        {
            ready = false;
            StartCoroutine("checkProximity");
        }
        else
        {
             UtilsHabilidades.addHab(this);
            
        }

    }

    public void callDoAction(bool valido)
    {  
        if (!valido)
        {
            nextAction = 1f;
        }
        else if (nextAction <= 0)
        {
            doAction();
        }
    }

    public virtual void doAction()
    {
        // Este doAction se ejecutara a la vez que el de los hijos, aqui ira la lógica común de 
        // las acciones, como puede ser el nextAction = getCoolDown();
        nextAction = getCoolDown();
        coolDownAzul();
    }

    private void coolDownAzul()
    {
        if (button && player.CompareTag("Player") && !isPasiva)
        {
            Color myColor = new Color();
            ColorUtility.TryParseHtmlString("#14B3BAFF", out myColor);
            myColor.a = 0.4F;
            image.color = myColor;
        }
    }

    IEnumerator checkProximity()
    {
        for (;;)
        {
          float proximity = player.transform.position.x - player.getEnemy().transform.position.x;
          proximity = Mathf.Abs(proximity);
         
           if (proximity < maxProximity)
            {
                UtilsHabilidades.addHab(this);
                ready = true;
                yield break;
            }
            //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
            yield return new WaitForSeconds(.1f);
        }       
    }


    public virtual void decalajeHabilidad()
    {
        if (nextAction < 1f && functional && player.canMove() && ready)
        {
           nextAction = 1f;

            if (button && player.CompareTag("Player") && !isPasiva)
            {       
                Color myColor = new Color();
                ColorUtility.TryParseHtmlString("#0081cb", out myColor);
                image.color = myColor;

            }
        }
    }

    public virtual bool gestionarHabPrioridad(int prioridadActual)
    {
        if (prioridad < prioridadActual && nextAction < 1 && player.canMove() && functional && ready)
        {   
            return false;
        }
        else
        {  
            return true;
        }
    }

    public bool getIsPlayer()
    {
      return player.gameObject.tag.Equals("Enemy") ? false : true;
    }

    public float getCoolDown()
    {
        return coolDown;
    }

    public void setCoolDown(float coolDown)
    {
        this.coolDown = coolDown;
    }

    public void setButtonScript(ButtonScript buttonScript)
    {
        button = buttonScript;
    }

    public ButtonScript getButtonScript()
    {
        return button;
    }

    public PlayerScript getPlayer()
    {
        return player;
    }

    public void setFunctional(bool functional)
    {
        this.functional = functional;
    }

    public void setMaxProximity(float maximaX)
    {
        maxProximity = maximaX;
    }

    public void setDamage(float varDdamage)
    {
        this.damage = varDdamage;
    }
    public float getDamage()
    {
        return damage;
    }

    public void setAlcance(int varAlcance)
    {
        alcance = varAlcance;
    }
    public int getAlcance()
    {
        return alcance;
    }

    public void enablePlayerRightHand()
    {
        player.enableAnimationRightHand(true);
    }

    public void boxColliderOffset()
    {
        GetComponent<BoxCollider2D>().offset = getDistancia();

    }

    public Vector2 getDistancia()
    {
        print(alcance);
        switch (alcance)
        {
            case 1:
                return new Vector2(0.1f, 0f);
        
            case 2:
                return  new Vector2(0.3f, 0f);
               
            case 3:
                return  new Vector2(0.6f, 0f);
               

        }
        return new Vector2(0.1f,0f);
    }

}
