using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot : MonoBehaviour
{
    public float CurHp = 100f;
    private float MaxHp = 100f;
    public Canvas uiCanvas;
    private Image hpbarImage;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 1.0f, 0);
    private void Start()
    {
        CurHp = 100;
        SetHpBar();
    }
    private void SetHpBar()
    {
        GameObject hpbar = Instantiate(hpBarPrefab, uiCanvas.transform);
        hpbarImage = hpbar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpbar.GetComponent<Hpbar>();
        _hpbar.target = gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Debug.Log(CurHp);
            CurHp -= collision.gameObject.GetComponent<Sword>().damage;
            hpbarImage.fillAmount = CurHp / MaxHp;
            if(CurHp <= 0)
            {
                CurHp = 100F;
            }
        }
    }
}


