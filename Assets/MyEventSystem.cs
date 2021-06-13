using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MyEventSystem
{

    public static System.Action<int> damagedWall;
    public static System.Action<int> enemyDead;
    public static System.Action<int> laserCreated;
    public static System.Action<int> fusion;

}