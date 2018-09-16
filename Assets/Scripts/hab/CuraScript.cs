using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CuraScript : WeaponScript
{
    private Image image;
    private Text text;
    private bool activo = false;
    private PlayerScript owner;
    public int curacion;
    private Color colorOriginal;


    public new void Start () {
        base.Start();

        setFunctional(true);
        owner = GetComponentInParent<PlayerScript>();

        if (getButtonScript() && player.CompareTag("Player"))
        {
            image = new List<Image>(button.GetComponentsInChildren<Image>()).Where(i => i.tag == "button").Single<Image>();
            colorOriginal = image.color;
            text = new List<Text>(button.GetComponentsInChildren<Text>()).Where(i => i.tag == "Cooldown").Single<Text>();
            activo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            base.utilCalculateCooldown();
        }
        else
        {
            doAction();
        }
    }

    public override void doAction()
    {
        if (owner.actLife + curacion < owner.maxLife) {
            //Ejecuta el doAction() del padre para actualizar la variable "nextAction"
            base.doAction();
            owner.actLife += curacion;
            ready = true;
            if (image != null)
            {
                image.color = colorOriginal;
            }
        }
        else
        {
            ready = false;
            if (image != null)
            {
                image.color = Color.green;
            }
        }
    }

    public void setCuracion(int curacion)
    {
        this.curacion = curacion;
    }
}
