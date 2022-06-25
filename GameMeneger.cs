using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger
{

    private static int _globalCountMoney = 0;
    //private readonly float _Katac;
    //private readonly float _Kspeed;
    //private readonly float _Kjump

    public static void AddCountMoney(int count)
    {
        _globalCountMoney += count;
    }
    public static int GetGloabalCountMoney()
    {
        return _globalCountMoney;
    }
}

