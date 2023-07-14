using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new weapon", menuName = "ForWeapon")] 
public class WeaponManager:ScriptableObject
{
    public int ammo;
    public int firerate;
    public int magzine;
    public float TimeToReload;
}
