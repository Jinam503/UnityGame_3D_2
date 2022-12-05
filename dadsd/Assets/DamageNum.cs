using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNum : MonoBehaviour
{
    private Camera uiCam;
    private Canvas canvas;
    private RectTransform rectParent;
    private Transform trans;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCam = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
        if (screenPos.z < 0.0f)
        {
            screenPos *= 1.0f;
        }
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCam, out localPos);
        trans.localPosition = localPos;
    }
}
