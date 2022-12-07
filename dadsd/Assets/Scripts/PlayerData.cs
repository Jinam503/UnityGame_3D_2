using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance
    {
        get
        {
            if( instance == null)
            {
               instance = FindObjectOfType<PlayerData>();
               if(instance == null)
                {
                    var gameObject = new GameObject("PlayerData");
                    instance = gameObject.AddComponent<PlayerData>();   
                }
            }
            return instance;
            
        }
    }
    private static PlayerData instance;

    public List<int> PlayerSkill = new List<int>();
    public int MeleeDamage = 10;
    public bool CanDamage = true;
}
