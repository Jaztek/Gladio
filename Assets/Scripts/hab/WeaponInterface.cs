using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  WeaponInterface {

    void doAction();

    float getCoolDown();

    void setButtonScript(ButtonScript button);

    void decalajeHabilidad();

    bool gestionarHabPrioridad(int prioridadActual);

}
