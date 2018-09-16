using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Linq;
using System.Net.Sockets;

public class QueryMaster{

    private static MongoClient client = new MongoClient("mongodb://admin:admin@192.168.1.100:27017/gladio");
    private static MongoServer server = client.GetServer();
    private static MongoDatabase db = server.GetDatabase("gladio");
    private static MongoCollection<PlayerTemplate> playercollection;

    private static bool isOnline()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IAsyncResult result = socket.BeginConnect("192.168.1.100", 27017, null, null);

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
    }
    public static GameObject findRandomEnemy()
    {
        if (!isOnline()) {
            UtilsPlayer.print("Sin conexión a BBDD");
            return null;
        }
        playercollection = db.GetCollection<PlayerTemplate>("usuarios");
        var whereNotMe = Query.Not(new QueryDocument("_id", SaveLoad.playerTemplate.Id));
        PlayerTemplate enemyTemplate = playercollection.Find(whereNotMe).ElementAt(UnityEngine.Random.Range(0, Convert.ToInt32(playercollection.Find(whereNotMe).Count())));
       
        GameObject enemy = UtilsPlayer.buildEnemy(enemyTemplate);

        return enemy;
    }

    public static PlayerTemplate updatePlayer(PlayerTemplate playerTemp)
    {
        if (!isOnline())
        {
            UtilsPlayer.print("Sin conexión a BBDD");
            return playerTemp;
        }

        playercollection = db.GetCollection<PlayerTemplate>("usuarios");
        playercollection.Save(playerTemp);

        return playerTemp;
    }
}
