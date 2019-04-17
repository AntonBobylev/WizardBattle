using System;
using System.Reflection;
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
        MethodInfo method = GetType().GetMethod(_shape.ToString() + _target.ToString());
        if (method != null)
        {
            method.Invoke(this, null);
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
    public Fire(GameObject _caster) : base(_caster) {}
    
    public void StreamForward()
    {
        Debug.Log("Fire Stream Forward");
    }
    public void ProjectileForward()
    {
        Debug.Log("Fire Projectile Forward");
    }
    public void ProjectileAround()
    {
        Debug.Log("Fire Projectile Around");
    }
}
public class Frost : Element
{
    public Frost(GameObject _caster) : base(_caster) { }

    public void StreamForward()
    {
        Debug.Log("Frost Stream Forward");
    }
    public void ProjectileForward()
    {
        Debug.Log("Frost Projectile Forward");
    }
    public void ProjectileAround()
    {
        Debug.Log("Frost Projectile Around");
    }
}
