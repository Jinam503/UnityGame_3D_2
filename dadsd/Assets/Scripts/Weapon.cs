using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Sword, Gun };
    public Type type;
    public int damage;
    public float rate; //attack speed
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    
    public void Use()
    {
        if(type == Type.Sword)
        {
            StopCoroutine(Swing());
            StartCoroutine(Swing());
        }
    }
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(0.05f);
        meleeArea.enabled = false;
        trailEffect.enabled = false;

    }
}
