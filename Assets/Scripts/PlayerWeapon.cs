using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serializeble makes it possible to be used in other classes in Unity
[System.Serializable]
public class PlayerWeapon {

    public string name = "Glock";
    public int damage = 10;
    public float range = 100f;

}
