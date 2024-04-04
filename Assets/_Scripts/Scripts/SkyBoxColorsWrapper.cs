using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SkyBoxColorsWrapper", menuName = "Colors", order = 1)]
public class SkyBoxColorsWrapper : ScriptableObject
{
    [Serializable]
    public class SkyBoxColors
    {
        public int index;
        public Color[] colors;
        public Sprite skyBox;
    }

    public SkyBoxColors[] SkyBoxColorsArray;
}
