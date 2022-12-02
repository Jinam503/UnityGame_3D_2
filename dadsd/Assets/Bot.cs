using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot : MonoBehaviour
{
    public Canvas uiCanvas;
    private Image hpbarImage;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private void Start()
    {
        SetHpBar();
    }
    void SetHpBar()
    {
        GameObject hpbar = Instantiate(hpBarPrefab, uiCanvas.transform);
        hpbarImage = hpbar.GetComponentsInChildren<Image>()[1];

        var _hpbar = hpbar.GetComponent<Hpbar>();
        _hpbar.target = gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }

}


