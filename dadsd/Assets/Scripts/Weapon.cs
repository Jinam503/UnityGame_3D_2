using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type ( Sword, Gun );
    public Type type;
    public int damage;
    public float rate; //attack speed
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
