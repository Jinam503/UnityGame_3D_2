using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot : MonoBehaviour
{
    public float CurHp;
    public float MaxHp;

    Rigidbody rigid;
    BoxCollider boxCollider;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            CurHp -= weapon.damage;
        }
    }
}


