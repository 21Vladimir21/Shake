using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class FinishPipe : MonoBehaviour
{
    [field: SerializeField] public Transform Entry { get; private set; }
    [SerializeField] private Transform rotatePivot;
    [SerializeField]private Color white = Color.white;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float rotateValue = 340;
    [SerializeField] private float uppingValue = 1f;
    [SerializeField] public float moveToPipeTime;
    [SerializeField] private bool clockwise;

    [SerializeField] private Animator chest;

    [SerializeField] private GameObject particle;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private Material _clonedMaterial;
    private Color _originalColor;

    private const string Open_Chest_Key = "Open";

    private void Awake()
    {
        CreateCloneMaterial();
        SaveAndSetNewColor();
    }
    

    public void EnableCamera(Transform target)
    {
        _camera.LookAt = target;
        _camera.gameObject.SetActive(true);
    }

    public void StartRotate(Transform pivot, Action callback)
    {
        if (chest) chest.SetBool(Open_Chest_Key, true);
        if (particle) particle.gameObject.SetActive(true);
        _clonedMaterial.color = _originalColor;
        pivot.SetParent(rotatePivot);
        rotatePivot.DORotate(new Vector3(0, clockwise ? rotateValue : -rotateValue, 0), rotationSpeed,
                RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
            .OnComplete(() => callback?.Invoke());
        rotatePivot.DOMoveY(rotatePivot.position.y + uppingValue, rotationSpeed);
    }

    private void CreateCloneMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();
        _clonedMaterial = Instantiate(renderer.material);
        renderer.material = _clonedMaterial;
    }

    private void SaveAndSetNewColor()
    {
        _originalColor = _clonedMaterial.color;
        _clonedMaterial.color = white;
    }
}