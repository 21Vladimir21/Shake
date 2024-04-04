using System;
using UnityEngine;

public class Common : LevelVizualization
{
    [SerializeField] private Sprite _commonOff;
    [SerializeField] private Sprite _commonOn;
    
    public override void InitializeText(int elementOrder)
    {
        SetOrder(elementOrder);
    }

    public override void InitializeImage()
    {
        _image.sprite = _commonOff;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log(name + " Activated" );
        _image.sprite = _commonOn;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Debug.Log(name + " Deactivated" );
        _image.sprite = _commonOff;
    }
}
