using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory {


    public static PlayerCore getPlayerCore(PlayerTemplate player)
    {
        if (player.playerCore == null)
        {
            player.playerCore = new PlayerCore();
        }
       
        return buildPlayerCoreByActivo(player);
    }

    private static PlayerCore buildPlayerCoreByActivo(PlayerTemplate player)
    {
        

        int activo = player.conjuntoActivo;
       
        switch (activo)
        {
            case 1:

                player.playerCore.equip = player.itemConjunto1;
                player.playerCore.weapon = player.weaponConjunto1;
                player.playerCore.habilidades = player.habilidadesConjunto1;
                break;

            case 2:
                player.playerCore.equip = player.itemConjunto2;
                player.playerCore.weapon = player.weaponConjunto2;
                player.playerCore.habilidades = player.habilidadesConjunto2;

                break;
            case 3:
                player.playerCore.equip = player.itemConjunto3;
                player.playerCore.weapon = player.weaponConjunto3;
                player.playerCore.habilidades = player.habilidadesConjunto3;

                break;

        }

        return player.playerCore;
    }
    public static PlayerTemplate getPlayerTemplate(PlayerCore core)
    {
        PlayerTemplate player = new PlayerTemplate();

        player.itemConjunto1 = core.equip;
        player.weaponConjunto1 = core.weapon;
        player.habilidadesConjunto1 = core.habilidades;

        player.playerCore = core;

        return player;
    }
}
