using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponPlayerPanel : MonoBehaviour, IDropHandler
{

    private PlayerScript player;

    private WeaponTemplate weapTemp;


    public void OnDrop(PointerEventData eventData)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        WeaponTemplate weaponTemplate = eventData.pointerDrag.gameObject.GetComponentInChildren<WeaponSingle>().getWeapon();
        player.setWeaponTo(weaponTemplate);
        weapTemp = weaponTemplate;

        SaveLoad.saveWeaponConjunto1(weapTemp);
    }

    public WeaponTemplate getWeaponTemplate()
    {
        return weapTemp;
    }

}
