using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ReduccionDanoPasivaScript : PasivaScript
{
    private Image image;
    private Text text;
    private bool activo = false;
    
    
    new public void Start () {
        setFunctional(false);
        player = this.GetComponentInParent<PlayerScript>();

        if (getButtonScript() && player.CompareTag("Player"))
        {
            image = new List<Image>(button.GetComponentsInChildren<Image>()).Where(i => i.tag == "button").Single<Image>();
            text = new List<Text>(button.GetComponentsInChildren<Text>()).Where(i => i.tag == "Cooldown").Single<Text>();
            inicializarHabilidad();
        }

      
    }

    private void inicializarHabilidad()
    {
        image.color = Color.green;
        text.text = "P";
    }

    private void Update()
    {
        if (getButtonScript() && player.CompareTag("Player"))
        {
            if (player.getVidaActual() > 0)
            {
                //Si el jugador baja del 25% de la vida aplica la pasiva
                if (player.getVidaMaxima() / player.getVidaActual() > 4)
                {
                    image.color = Color.blue;
                    if (!activo)
                    {
                        player.aplicarReduccionDaño(0.50f);
                        activo = true;
                    }
                }
                else
                {
                    image.color = Color.green;
                    if (activo)
                    {
                        player.aplicarReduccionDaño(-0.50f);
                        activo = false;
                    }
                }
            }
        }
    }
}
