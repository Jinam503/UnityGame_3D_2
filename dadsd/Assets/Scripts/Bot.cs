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
    public Vector3 DamageNumOffset;
    public GameObject damageImage;
    private void Start()
    {
        CurHp = 100;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Debug.Log(CurHp);
            CurHp -= PlayerData.Instance.MeleeDamage;
            hpbarImage.fillAmount = CurHp / MaxHp;
            if(CurHp <= 0)
            {
                CurHp = 100F;
            }
        }
        StartCoroutine(ShowDamage(PlayerData.Instance.MeleeDamage));
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


