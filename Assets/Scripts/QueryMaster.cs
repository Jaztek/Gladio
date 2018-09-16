using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

public class QueryMaster{

    // 84.120.71.34
    private static MongoClient client = new MongoClient("mongodb://gladio:javiyjc2!@ds137650.mlab.com:37650/gladio");
    private static MongoServer server = client.GetServer();
    private static MongoDatabase db = server.GetDatabase("gladio");
    private static MongoCollection<PlayerCore> playercollection;

    private static bool isOnline()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            IAsyncResult result = socket.BeginConnect("ds137650.mlab.com", 37650, null, null);

            //Si en 2 segundos no se conecta da la conexión por fallida
            bool success = result.AsyncWaitHandle.WaitOne(2000, true);

            if (socket.Connected)
            {

                return true;
            }
            else
            {
                socket.Close();
                return false;
            }
        }catch(Exception e)
        {
            socket.Close();
            return false;
        }
    }
    public static GameObject findRandomEnemy()
    {
        //!isOnline()
        if (!isOnline()) {
            UtilsPlayer.print("Sin conexión a BBDD");
            return UtilsPlayer.buildEnemy(null);
        }
        playercollection = db.GetCollection<PlayerCore>("usuarios");
        var whereNotMe = Query.Not(new QueryDocument("_id", SaveLoad.playerTemplate.playerCore.Id));
        PlayerCore core = playercollection.Find(whereNotMe).ElementAt(UnityEngine.Random.Range(0, Convert.ToInt32(playercollection.Find(whereNotMe).Count())));

        PlayerTemplate enemyTemplate = PlayerFactory.getPlayerTemplate(core);
        return UtilsPlayer.buildEnemy(enemyTemplate);
    }

    public static PlayerCore updatePlayer(PlayerCore player)
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");
            return player;
        }
        playercollection = db.GetCollection<PlayerCore>("usuarios");

        playercollection.Save(player);

        return player;
    }

    public static List<ItemMaestro> getMaestroItems()
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");
            return new List<ItemMaestro>();
        }
        MongoCollection<ItemMaestro> itemCollection = db.GetCollection<ItemMaestro>("maestro_items");

        List<ItemMaestro> items = itemCollection.FindAll().ToList<ItemMaestro>();

        return items;
    }

    public static List<WeaponMaestro> getMaestroWeapons()
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");
            return new List<WeaponMaestro>();
        }
        MongoCollection<WeaponMaestro> weapCollection = db.GetCollection<WeaponMaestro>("maestro_weapon");

        List<WeaponMaestro> weaps = weapCollection.FindAll().ToList<WeaponMaestro>();

        return weaps;

        // MongoCursor<String> cursor = collection.iterator();

    }


    public static void scriptInsertItem()
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");
           
        }

        UtilsPlayer.print("actualiza Items");
        MongoCollection<ItemMaestro> itemCollection = db.GetCollection<ItemMaestro>("maestro_items");

        ItemMaestro item = new ItemMaestro();
        item.identificador = "head1";
        item.sprite = "head1";
        item.portentaje = 10;
        item.posicion = 1;

        itemCollection.Insert(item);

        ItemMaestro item2 = new ItemMaestro();
        item2.identificador = "head2";
        item2.sprite = "head2";
        item2.portentaje = 10;
        item2.posicion = 1;

        itemCollection.Insert(item2);

        ItemMaestro item3 = new ItemMaestro();
        item3.identificador = "head3";
        item3.sprite = "head3";
        item3.portentaje = 10;
        item3.posicion = 1;

        itemCollection.Insert(item3);

        ItemMaestro item4 = new ItemMaestro();
        item4.identificador = "head4";
        item4.sprite = "head4";
        item4.portentaje = 10;
        item4.posicion = 1;

        itemCollection.Insert(item4);

        ItemMaestro item5 = new ItemMaestro();
        item5.identificador = "head5";
        item5.sprite = "head5";
        item5.portentaje = 10;
        item5.posicion = 1;

        itemCollection.Insert(item5);

        ItemMaestro item6 = new ItemMaestro();
        item6.identificador = "tronco1";
        item6.sprite = "tronco1";
        item6.portentaje = 10;
        item6.posicion = 2;

        itemCollection.Insert(item6);

        ItemMaestro item7 = new ItemMaestro();
        item7.identificador = "tronco2";
        item7.sprite = "tronco2";
        item7.portentaje = 10;
        item7.posicion = 2;

        itemCollection.Insert(item7);

        ItemMaestro item8 = new ItemMaestro();
        item8.identificador = "tronco3";
        item8.sprite = "tronco3";
        item8.portentaje = 10;
        item8.posicion = 2;

        itemCollection.Insert(item8);

        ItemMaestro item9 = new ItemMaestro();
        item9.identificador = "tronco4";
        item9.sprite = "tronco4";
        item9.portentaje = 10;
        item9.posicion = 2;

        itemCollection.Insert(item9);

        ItemMaestro item10 = new ItemMaestro();
        item10.identificador = "tronco5";
        item10.sprite = "tronco5";
        item10.portentaje = 10;
        item10.posicion = 2;

        itemCollection.Insert(item10);

        ItemMaestro item11 = new ItemMaestro();
        item11.identificador = "zapas1";
        item11.sprite = "zapas1";
        item11.portentaje = 10;
        item11.posicion =3;

        itemCollection.Insert(item11);

        ItemMaestro item12 = new ItemMaestro();
        item12.identificador = "zapas2";
        item12.sprite = "zapas2";
        item12.portentaje = 10;
        item12.posicion = 3;

        itemCollection.Insert(item12);

        ItemMaestro item13 = new ItemMaestro();
        item13.identificador = "zapas3";
        item13.sprite = "zapas3";
        item13.portentaje = 10;
        item13.posicion = 3;

        itemCollection.Insert(item13);

        ItemMaestro item14 = new ItemMaestro();
        item14.identificador = "zapas4";
        item14.sprite = "zapas4";
        item14.portentaje = 10;
        item14.posicion = 3;

        itemCollection.Insert(item14);

        ItemMaestro item15 = new ItemMaestro();
        item15.identificador = "zapas5";
        item15.sprite = "zapas5";
        item15.portentaje = 10;
        item15.posicion = 3;

        itemCollection.Insert(item15);

    }

    public static void scriptInsertWeapon()
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");

        }

        // public int tipoHabilidadBasica;

        // 1 corta, 2 media, 3 larga.
        //public int distancia;

        UtilsPlayer.print("actualiza weapon");
        MongoCollection<WeaponMaestro> weaponCollection = db.GetCollection<WeaponMaestro>("maestro_weapon");

        WeaponMaestro weap = new WeaponMaestro();
        weap.identificador = "arpon1";
        weap.sprite = "arpon1";
        weap.damage = 15;
        weap.force = 10;
        weap.velocidad = 10;
        weap.tipoHabilidadBasica = 1;
        weap.alcance = 3;

        weaponCollection.Insert(weap);


        WeaponMaestro weap1 = new WeaponMaestro();
        weap1.identificador = "axe1";
        weap1.sprite = "axe1";
        weap1.damage = 10;
        weap1.force = 10;
        weap1.velocidad = 15;
        weap1.tipoHabilidadBasica = 1;
        weap.alcance = 2;

        weaponCollection.Insert(weap1);


        WeaponMaestro weap2 = new WeaponMaestro();
        weap2.identificador = "axe2";
        weap2.sprite = "axe2";
        weap2.damage = 15;
        weap2.force = 10;
        weap2.velocidad = 15;
        weap2.tipoHabilidadBasica = 1;
        weap.alcance = 2;

        weaponCollection.Insert(weap2);


        WeaponMaestro weap4 = new WeaponMaestro();
        weap4.identificador = "dest1";
        weap4.sprite = "dest1";
        weap4.damage = 10;
        weap4.force = 10;
        weap4.velocidad = 25;
        weap4.tipoHabilidadBasica = 1;
        weap.alcance = 1;

        weaponCollection.Insert(weap4);

        WeaponMaestro weap5 = new WeaponMaestro();
        weap5.identificador = "mace1";
        weap5.sprite = "mace1";
        weap5.damage = 25;
        weap5.force = 20;
        weap5.velocidad = 5;
        weap5.tipoHabilidadBasica = 1;
        weap.alcance = 1;

        weaponCollection.Insert(weap5);


        UtilsPlayer.print("weapons insertadas");
    }
}
