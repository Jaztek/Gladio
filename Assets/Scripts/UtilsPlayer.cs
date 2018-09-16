using System.Collections.Generic;
using UnityEngine;

public class UtilsPlayer: MonoBehaviour
{
    public static List<WeaponInterface> habilitiesEnemy;
    public static List<WeaponInterface> habilitiesPlayer;

    public static GameObject buildPlayer(PlayerTemplate playerTempl)
    {
        GameObject player = null;
        //Añadimos las habilidades del enemigo sin necesidad de usar botones físicos
        if (playerTempl != null)
        {
            player = Resources.Load("Prefabs/Players/player") as GameObject;

            // Creacion de la carpteta de habilidades
            GameObject habilidades = new GameObject("habilidades");
            GameObject.Destroy(habilidades);
            habilidades = Instantiate(habilidades, player.transform);
            habilidades.transform.localPosition = new Vector3(0, 0, 0);
            habilidades.transform.localScale = new Vector3(1, 1, 1);

            // Creacion de la carpteta de habilidad basica del arma
            GameObject habilidadWeap = new GameObject("habilidadBasica");
            GameObject.Destroy(habilidadWeap);
            habilidadWeap = Instantiate(habilidadWeap, player.transform);
            habilidadWeap.transform.localPosition = new Vector3(0, 0, 0);
            habilidadWeap.transform.localScale = new Vector3(1, 1, 1);

            foreach (HabilidadTemplate hab in playerTempl.getHabilidadesConjunto1())
            {
                if (hab != null)
                {
                    GameObject hab1 = Resources.Load("Prefabs/Habilidades/" + hab.nombre) as GameObject;
                    hab1 = Instantiate(hab1, habilidades.transform);

                    WeaponScript script = hab1.GetComponent<WeaponScript>();
                    script.setMaxProximity(hab.maxProximity);
                    script.setCoolDown(hab.cooldown);
                    script.setDamage(hab.damage);
                    script.prioridad = hab.prioridad;
                    if (script is CuraScript)
                    {
                        (script as CuraScript).setCuracion(hab.curacion);
                    }
                }
            }

            foreach(ItemTemplate item in playerTempl.itemConjunto1)
            {
                item.itemMaestro = UtilsItems.getItemMaestro(item.identificador);
                if (item.itemMaestro != null)
                {
                    player.GetComponent<PlayerScript>().setSpriteTo(item);
                }
            }

            if (playerTempl.getWeaponConjunto1() != null)
            {
                WeaponTemplate weap = playerTempl.getWeaponConjunto1();

                weap.weaponMaestro = UtilsItems.getWeaponMaestro(weap.identificador);
              
                if (weap.identificador != null){
                    player.GetComponent<PlayerScript>().setWeaponTo(weap);

                    string habilidadW = UtilsWeapon.getWeaponHab(weap.weaponMaestro.tipoHabilidadBasica);
                    GameObject habW = Resources.Load("Prefabs/Habilidades/basicas/" + habilidadW) as GameObject;

                    habW = Instantiate(habW, habilidadWeap.transform);

                    WeaponScript script = habW.GetComponent<WeaponScript>();
                    script.setMaxProximity(5);
                    script.setCoolDown(weap.weaponMaestro.velocidad);
                    script.setDamage(weap.weaponMaestro.damage);
                    script.setAlcance(weap.weaponMaestro.alcance);
                    print(weap.weaponMaestro.alcance);

                    script.prioridad = 0;

                }

            }
            //Insertamos las habilidades básicas de las armas.

        }
        else
        {
            Vector3 posicionPlayer = new Vector3(-7f, -2.5f, 0f);
            player = Instantiate(Resources.Load("Prefabs/Players/player") as GameObject, posicionPlayer, new Quaternion());
        }

        return player;
    }

    public static GameObject buildEnemy(PlayerTemplate enemyTemplate)
    {
        GameObject enemyClone = null;
        //Añadimos las habilidades del enemigo sin necesidad de usar botones físicos
        if (enemyTemplate != null)
        {
            Vector3 posicionPlayer = new Vector3(-7f, -2.5f, 0f);
            Vector3 posicionEnemy = new Vector3(-posicionPlayer.x, posicionPlayer.y, posicionPlayer.z);
            enemyClone = Instantiate(Resources.Load("Prefabs/Players/enemy1") as GameObject, posicionEnemy, new Quaternion());
            GameObject habilidades = new GameObject("habilidades");
            GameObject.Destroy(habilidades);
            habilidades = Instantiate(habilidades, enemyClone.transform);
            habilidades.transform.localPosition = new Vector3(0, 0, 0);
            habilidades.transform.localScale = new Vector3(1, 1, 1);
            habilitiesEnemy = new List<WeaponInterface>();
            foreach (HabilidadTemplate hab in enemyTemplate.getHabilidadesConjunto1())
            {

                if (hab != null)
                {
                    GameObject hab1 = Resources.Load("Prefabs/Habilidades/" + hab.nombre) as GameObject;
                    hab1 = Instantiate(hab1, habilidades.transform);

                    WeaponScript script = hab1.GetComponent<WeaponScript>();
                    script.setMaxProximity(hab.maxProximity);
                    script.setCoolDown(hab.cooldown);
                    script.setDamage(hab.damage);
                    script.prioridad = hab.prioridad;
                    if (script is CuraScript)
                    {
                        (script as CuraScript).setCuracion(hab.curacion);
                    }

                    GameObject enemyButton = new GameObject("Button" + hab.nombre);
                    ButtonScript buttonEnem = enemyButton.AddComponent<ButtonScript>() as ButtonScript;
                    script.setButtonScript(buttonEnem);

                    habilitiesEnemy.Add(script);
                }
            }
            foreach (ItemTemplate item in enemyTemplate.itemConjunto1)
            {
                item.itemMaestro = UtilsItems.getItemMaestro(item.identificador);

                if (item.itemMaestro != null)
                {
                    enemyClone.GetComponent<PlayerScript>().setSpriteTo(item);
                }
            }
        }
        else
        {
            Vector3 posicionPlayer = new Vector3(-7f, -2.5f, 0f);
            Vector3 posicionEnemy = new Vector3(-posicionPlayer.x, posicionPlayer.y, posicionPlayer.z);
            enemyClone = Instantiate(Resources.Load("Prefabs/Players/enemy1") as GameObject, posicionEnemy, new Quaternion());
        }

        return enemyClone;
    }

    public static void printHabilidades(PlayerTemplate playerTempl)
    {
        foreach (HabilidadTemplate hab in playerTempl.getHabilidadesConjunto1())
        {
            print(hab.nombre);
        }
    }

   
}
