using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ReduccionDanoPasivaScript : PasivaScript
{

    public new void Start () {
        base.Start();
        
        setFunctional(false);

        if (getButtonScript() && player.CompareTag("Player"))
        {
            Debug.Log(button.GetComponentsInChildren<Image>());
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
        if (getButtonScript())
        {
            if (player.getVidaActual() > 0)
            {
                //Si el jugador baja del 25% de la vida aplica la pasiva
                if (player.getVidaMaxima() / player.getVidaActual() > 4)
                {
                    if (player.CompareTag("Player"))
                    {
                        image.color = Color.blue;
                    }
                    if (ready)
                    {
                        player.aplicarReduccionDaño(0.50f);
                        ready = false;
                    }
                }
                else
                {
                    if (player.CompareTag("Player"))
                    {
                        image.color = Color.green;
                    }
                    if (!ready)
                    {
                        player.aplicarReduccionDaño(-0.50f);
                        ready = true;
                    }
                }
            }
        }
    }
}
