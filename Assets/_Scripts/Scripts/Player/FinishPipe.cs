using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class FinishPipe : MonoBehaviour
{
    [field: SerializeField] public Transform Entry { get; private set; }
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float rotateValue = 340;
    [SerializeField] private bool clockwise;
    [SerializeField] public float moveToPipeTime;
    

    [SerializeField] private GameObject particle;
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    


    public void StartRotate(Transform pivot, Action callback)
    {
        _camera.LookAt = pivot;
        _camera.gameObject.SetActive(true);
        particle.gameObject.SetActive(true);
        pivot.SetParent(rotatePivot);
        rotatePivot.DORotate(new Vector3(0, clockwise ? rotateValue : -rotateValue, 0), rotationSpeed,
                RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
            .OnComplete(() => callback?.Invoke());
        rotatePivot.DOMoveY(rotatePivot.position.y + 1f, rotationSpeed);
    }
}