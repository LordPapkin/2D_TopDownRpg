using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage 
{
    public Damage(Vector3 _origin, int _dmg, float _pushForce)
    {
        origin = _origin;
        damageAmount = _dmg;
        pushForce = _pushForce;
    }

    public Vector3 origin;
    public int damageAmount;
    public float pushForce;
}
