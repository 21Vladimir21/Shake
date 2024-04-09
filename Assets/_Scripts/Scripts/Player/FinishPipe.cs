using System;
using DG.Tweening;
using UnityEngine;


public class FinishPipe : MonoBehaviour
{
    [field: SerializeField] public Transform Entry { get; private set; }
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float rotateValue = 340;
    [SerializeField] private bool clockwise;

    [SerializeField] private GameObject particle;
    


    public void StartRotate(Transform pivot, Action callback)
    {
        particle.gameObject.SetActive(true);
        pivot.SetParent(rotatePivot);
        rotatePivot.DORotate(new Vector3(0, clockwise ? rotateValue : -rotateValue, 0), rotationSpeed,
                RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
            .OnComplete(() => callback?.Invoke());
        rotatePivot.DOMoveY(rotatePivot.position.y + 1f, rotationSpeed);
    }
}