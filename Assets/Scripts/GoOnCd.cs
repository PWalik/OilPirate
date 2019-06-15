using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOnCd : MonoBehaviour
{
    [SerializeField]
    Color onCDcolor, offCDcolor, disableColor;
    public bool isOnCD;
    MeshRenderer crosshairRender;
    bool isDisabled;
    public bool IsDisabled { get => isDisabled; }

    private void Start()
    {
        crosshairRender = GetComponent<MeshRenderer>();
    }

    public void TurnCD(bool isOn)
    {
        isOnCD = isOn;
        Color col = offCDcolor;
        if (isOn)
            col = onCDcolor;
        crosshairRender.material.EnableKeyword("_BaseColor");
        crosshairRender.material.SetColor("_BaseColor", col);

    }

    public void SetDisabled(bool isToDisable)
    {
        isDisabled = isToDisable;
        Color col = offCDcolor;
        if (isToDisable)
            col = disableColor;
        crosshairRender.material.EnableKeyword("_BaseColor");
        crosshairRender.material.SetColor("_BaseColor", col);
    }
}
