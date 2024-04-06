using System;
using DG.Tweening;
using UnityEngine;

public abstract class Food : MonoBehaviour
{
    [SerializeField] protected FoodData _foodData;
    [SerializeField] private ParticleSystem _foodFX;
    [SerializeField] private Transform _foodModel;

    private void Start()
    {
    }

    protected void FoodEaten()
    {
        if(_foodFX != null)
            _foodFX.Play();
        if (_foodModel != null)
            Destroy(_foodModel.gameObject);
        //DOTween.KillAll(gameObject);
    }
}
