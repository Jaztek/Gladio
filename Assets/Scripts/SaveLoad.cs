using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public static class SaveLoad
{
    public static PlayerTemplate playerTemplate;

    public static void saveHabilidadConjunto1(HabilidadTemplate habTemp)
    {
        List<HabilidadTemplate> habilidades = SaveLoad.getPlayerTemp().getHabilidadesConjunto1();
        HabilidadTemplate habCambiada = null;
        habilidades.ForEach(h => {
            if (h.prioridad == habTemp.prioridad)
            {
                habCambiada = h;
            }
        });
        if (habCambiada != null)
        {
            habilidades.Remove(habCambiada);
        }
        habilidades.Add(habTemp);
        playerTemplate.setHabilidadesConjunto1(habilidades);
        savePlayerTemp(playerTemplate);
    }

    public static void removeHabilidadConjunto1(HabilidadTemplate habTemp)
    {
        List<HabilidadTemplate> habilidades = SaveLoad.getPlayerTemp().getHabilidadesConjunto1();
        habilidades.Remove(habTemp);
        playerTemplate.setHabilidadesConjunto1(habilidades);
        savePlayerTemp(playerTemplate);
    }

    public static void saveItemsConjunto1(ItemTemplate itemTemp)
    {
        List<ItemTemplate> items = SaveLoad.getPlayerTemp().getItemsConjunto1();
        ItemTemplate itemCambiado = null;
        items.ForEach(i => {
            if (i.itemMaestro.posicion == itemTemp.itemMaestro.posicion)
            {
                itemCambiado = i;
            }
        });
        if (itemCambiado != null)
        {
            items.Remove(itemCambiado);
        }
        items.Add(itemTemp);
        playerTemplate.setItemsConjunto1(items);
        savePlayerTemp(playerTemplate);    
    }

    public static void saveWeaponConjunto1(WeaponTemplate weapon)
    {
        playerTemplate.setWeaponConjunto1(weapon);
        savePlayerTemp(playerTemplate);
    }

    public static void savePlayer()
    {
        try
        {
            getPlayerTemp().playerCore = QueryMaster.updatePlayer(PlayerFactory.getPlayerCore(getPlayerTemp()));
        }
        catch (Exception e)
        {
            UtilsPlayer.print("Error en la BBDD: updatePlayer -> " + e);
        }
       

        savePlayerTemp(playerTemplate);
    }

    private static void savePlayerTemp(PlayerTemplate playerTemp)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, playerTemp);
        playerTemplate = playerTemp;
       
        file.Close();
    }

    public static GameObject Load()
    {
        GameObject player = null;
        // File.Delete(Application.persistentDataPath + "/savedGames.gd");
      //  if (false)
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            PlayerTemplate playerTempl = (PlayerTemplate)bf.Deserialize(file);
            file.Close();
            file.Dispose();

            playerTemplate = playerTempl;
        }
        else
        {
            playerTemplate = new PlayerTemplate();
        }
        player = UtilsPlayer.buildPlayer(playerTemplate);
        return player;
    }

    public static void clearSave()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");
        }
    }

    public static void chargeLoot()
    {
 
        List<HabilidadTemplate> listHabilid = new List<HabilidadTemplate>();

        //por defecto le metemos uno a piñon para las pruebas.
        HabilidadTemplate habilidaBuild1 = new HabilidadTemplate();
        habilidaBuild1.nombre = "habEspadazo";
        habilidaBuild1.maxProximity = 5;
        habilidaBuild1.damage = 5;
        habilidaBuild1.cooldown = 5;
        listHabilid.Add(habilidaBuild1);

        HabilidadTemplate habilidaBuild2 = new HabilidadTemplate();
        habilidaBuild2.nombre = "habEstocada";
        habilidaBuild2.maxProximity = 5;
        habilidaBuild2.damage = 5;
        habilidaBuild2.cooldown = 5;
        listHabilid.Add(habilidaBuild2);

        HabilidadTemplate habilidaBuild3 = new HabilidadTemplate();
        habilidaBuild3.nombre = "habEscudo";
        habilidaBuild3.maxProximity = 5;
        habilidaBuild3.damage = 5;
        habilidaBuild3.cooldown = 5;
        listHabilid.Add(habilidaBuild3);

        HabilidadTemplate habilidaBuild4 = new HabilidadTemplate();
        habilidaBuild4.nombre = "habSalto";
        habilidaBuild4.maxProximity = 5;
        habilidaBuild4.damage = 5;
        habilidaBuild4.cooldown = 5;
        listHabilid.Add(habilidaBuild4);

        HabilidadTemplate habilidaBuild5 = new HabilidadTemplate();
        habilidaBuild5.nombre = "habArrowBasic";
        habilidaBuild5.maxProximity = 15;
        habilidaBuild5.damage = 5;
        habilidaBuild5.cooldown = 5;
        listHabilid.Add(habilidaBuild5);

        HabilidadTemplate habilidaBuild6 = new HabilidadTemplate();
        habilidaBuild6.nombre = "habReduccionDano";
        listHabilid.Add(habilidaBuild6);

        HabilidadTemplate habilidaBuild7 = new HabilidadTemplate();
        habilidaBuild7.nombre = "habCura";
        habilidaBuild7.curacion = 20;
        habilidaBuild7.cooldown = 25;
        listHabilid.Add(habilidaBuild7);

        HabilidadTemplate habilidaBuild8 = new HabilidadTemplate();
        habilidaBuild8.nombre = "habPasoSombras";
        habilidaBuild8.maxProximity = 15;
        habilidaBuild8.damage = 10;
        habilidaBuild8.cooldown = 10;
        listHabilid.Add(habilidaBuild8);



        getPlayerTemp().addHabilidades(listHabilid);


        List<ItemTemplate> listItem= new List<ItemTemplate>();
        ItemTemplate item1 = new ItemTemplate();
        item1.identificador = "head1";
        item1.itemLevel = 1;
        listItem.Add(item1);

        ItemTemplate item2 = new ItemTemplate();
        item2.identificador = "head2";
        item2.itemLevel = 1;
        listItem.Add(item2);

        ItemTemplate item3 = new ItemTemplate();
        item3.identificador = "head3";
        item3.itemLevel = 1;
        listItem.Add(item3);

        ItemTemplate item4 = new ItemTemplate();
        item4.identificador = "head4";
        item4.itemLevel = 1;
        listItem.Add(item4);

        ItemTemplate item5 = new ItemTemplate();
        item5.identificador = "tronco1";
        item5.itemLevel = 1;
        listItem.Add(item5);

        ItemTemplate item6 = new ItemTemplate();
        item6.identificador = "tronco2";
        item6.itemLevel = 1;
        listItem.Add(item6);

        ItemTemplate item7 = new ItemTemplate();
        item1.identificador = "tronco3";
        item7.itemLevel = 1;
        listItem.Add(item7);

        ItemTemplate item8 = new ItemTemplate();
        item8.identificador = "tronco4";
        item8.itemLevel = 1;
        listItem.Add(item8);

        ItemTemplate item9 = new ItemTemplate();
        item9.identificador = "zapas1";
        item9.itemLevel = 1;
        listItem.Add(item9);

        ItemTemplate item10 = new ItemTemplate();
        item10.identificador = "zapas2";
        item10.itemLevel = 1;
        listItem.Add(item10);

        ItemTemplate item11 = new ItemTemplate();
        item11.identificador = "zapas3";
        item11.itemLevel = 1;
        listItem.Add(item11);

        ItemTemplate item12 = new ItemTemplate();
        item12.identificador = "zapas4";
        item12.itemLevel = 1;
        listItem.Add(item12);

        getPlayerTemp().addItems(listItem);


        List<WeaponTemplate> listWeap = new List<WeaponTemplate>();

        WeaponTemplate weap = new WeaponTemplate();
        weap.identificador = "arpon1";
        weap.weaponLevel = 1;
        listWeap.Add(weap);

        WeaponTemplate weap1 = new WeaponTemplate();
        weap1.identificador = "axe1";
        weap1.weaponLevel = 1;
        listWeap.Add(weap1);

        WeaponTemplate weap2 = new WeaponTemplate();
        weap2.identificador = "axe2";
        weap2.weaponLevel = 1;
        listWeap.Add(weap2);

        WeaponTemplate weap3 = new WeaponTemplate();
        weap3.identificador = "dest1";
        weap3.weaponLevel = 1;
        listWeap.Add(weap3);

        WeaponTemplate weap4 = new WeaponTemplate();
        weap4.identificador = "mace1";
        weap4.weaponLevel = 1;
        listWeap.Add(weap4);

        getPlayerTemp().addWeapons(listWeap);


        savePlayerTemp(playerTemplate);
    }

    public static List<HabilidadTemplate> getHabilities()
    {
        return getPlayerTemp().getHabilidades();
    }
    public static List<ItemTemplate> getItems()
    {
        return getPlayerTemp().getItems();
    }

    public static List<WeaponTemplate> getWeapons()
    {
        return getPlayerTemp().getWeapons();
    }

    public static PlayerTemplate getPlayerTemp()
    {
        if (playerTemplate == null)
        {
            Load();
        }
        return playerTemplate;
    }
}
