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


    //체력바 띄우기
    public Canvas uiCanvas;
    private Image hpbarImage;
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 1.0f, 0);
    public Vector3 DamageNumOffset;
    public GameObject damageImage;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        SetHpBar();
    }
    private void Update()
    {
        DamageNumOffset = new Vector3(Random.Range(1.0f, 0f), Random.Range(1.0f, 0f), 0);
    }
    private void SetHpBar()
    {
        GameObject hpbar = Instantiate(hpBarPrefab, uiCanvas.transform);
        hpbarImage = hpbar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpbar.GetComponent<Hpbar>();
        _hpbar.target = gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            CurHp -= weapon.damage;
            StartCoroutine(ShowDamage(weapon.damage));
        }
    }
    
    IEnumerator ShowDamage(int damage)
    {
        GameObject damageNum = Instantiate(damageImage, uiCanvas.transform);
        TextMesh tM = damageNum.GetComponent<TextMesh>();
        string numString = "" + damage;
        tM.text = numString;
        var _dN = damageNum.GetComponent<DamageNum>();
        _dN.target = gameObject.transform;
        _dN.offset = DamageNumOffset;
        yield return new WaitForSeconds(2f);
        Destroy(damageNum);
    }
}


