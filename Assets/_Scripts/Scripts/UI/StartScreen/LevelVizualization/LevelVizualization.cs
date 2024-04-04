using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelVizualization : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;
    [SerializeField] protected Image _image;
    protected SpriteState _state = SpriteState.Disabled;

    public SpriteState State => _state;

    private const float FontSizeOffset = 8;

    public abstract void InitializeText(int elementOrder);
    public abstract void InitializeImage();

    public virtual void Activate()
    {
        _state = SpriteState.Active;
        _text.enableAutoSizing = false;
        _text.fontSize = _text.fontSize += FontSizeOffset;
    }

    public virtual void Deactivate()
    {
        _state = SpriteState.Disabled;
        _text.enableAutoSizing = true;
        //_text.fontSize = _text.fontSize -= FontSizeOffset;
    }
    
    protected virtual void SetOrder(int elementOrder)
    {
        var levelCount = (ServiceLocator.LevelSpawner.CurrentLevel);
        var remainder =  levelCount % 6;
        if (levelCount is >= 1 and <= 6)
            levelCount = 1;
        else if (remainder != 1)
            levelCount -= (remainder - 1);
        Debug.Log(name + " " + (levelCount + elementOrder));
        _text.text = Convert.ToString(levelCount + elementOrder);
    }
}

public enum SpriteState
{
    Active,
    Disabled
}
