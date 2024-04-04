using System;
using UnityEngine;

public class Twerk : LevelVizualization
{
    [SerializeField] private Sprite _battleOff;
    [SerializeField] private Sprite _battleOn;

    public override void InitializeText(int elementOrder)
    {
        SetOrder(elementOrder);
    }

    public override void InitializeImage()
    {
        _image.sprite = _battleOff;
    }

    public override void Activate()
    {
        base.Activate();
        _image.sprite = _battleOn;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _image.sprite = _battleOff;
    }
}
