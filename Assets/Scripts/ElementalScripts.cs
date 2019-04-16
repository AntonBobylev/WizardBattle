using System;
using UnityEngine;

public abstract class Element
{
    GameObject caster;
    public Element(GameObject _caster)
    {
        caster = _caster;
    }
    public virtual void ParseCast(Magic.Shapes _shape, Magic.Targets _target)
    {
        //Debug it
        var method = GetType().GetMethod(Enum.GetName(typeof(Magic.Shapes), _shape) + Enum.GetName(typeof(Magic.Targets), _target));
        if (method != null)
        {
            method.Invoke(null, null);
        }
        else
        {
            UnscriptedCast();
        }
    }
    protected virtual void UnscriptedCast()
    {
        Debug.Log("Warning: unscricted cast");
    }
}

public class Fire : Element
{
    public Fire(GameObject _caster) : base(_caster)
    {

    }

    void StreamForward()
    {
        Debug.Log("Fire Stream Forward");
    }
    void ProjectileForward()
    {
        Debug.Log("Fire Projectile Forward");
    }
    void ProjectileAround()
    {
        Debug.Log("Fire Projectile Around");
    }
}
