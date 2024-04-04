using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCanvas : MonoBehaviour
{
    [SerializeField] private float _distanceToCamera;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private UnityEngine.Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SkyBoxColorsWrapper skyBoxColors;
    [SerializeField] private Image background;
    [SerializeField] private Material skyBoxMaterial;
    [SerializeField] private LevelSpawner spawner;
    
    private static readonly int SkyTint = Shader.PropertyToID("_SkyTint");
    private static readonly int GroundColor = Shader.PropertyToID("_GroundColor");

    private void Start()
    {
        UpdateVisual();
    }

    private void OnEnable()
    {
        spawner.LevelSpawned += UpdateVisual;
    }

    private void OnDisable()
    {
        spawner.LevelSpawned -= UpdateVisual;
    }

    private void UpdateVisual()
    {
        int index =  DataManager.Instance.CurrentLevel % skyBoxColors.SkyBoxColorsArray.Length;
        Debug.LogWarning("INDEX: " + index);
        var skyBoxArray = skyBoxColors.SkyBoxColorsArray;

        background.sprite = skyBoxArray[index].skyBox;
        skyBoxMaterial.SetColor(SkyTint, skyBoxArray[index].colors[0]);
        skyBoxMaterial.SetColor(GroundColor, skyBoxArray[index].colors[1]);
    } 


    private void Update()
    {
        _canvas.transform.eulerAngles = _rotation;
        Vector3 position = transform.position;
        position.z = _camera.transform.position.z + _distanceToCamera;
        _canvas.transform.position =  position;
    }
}
