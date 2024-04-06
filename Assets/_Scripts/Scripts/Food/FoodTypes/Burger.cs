using System;
using DG.Tweening;
using UnityEngine;

public class Burger : Food, IEatable
{
    private bool _canEat = true;
    [SerializeField] private Collider collider;
    

    // [SerializeField] private AudioClip _burgerEaten;
    private void OnDisable()
    {
        _canEat = false;
    }

    public void Eat(Player player)
    {
        
        if (enabled== false) return;
        player.TryApplyDamage(-_foodData.ValueShift);
        //player.PlayerAnimator.SetRunningTrigger();
        FoodEaten();
        // SoundManager.Instance.PlaySound(_burgerEaten);
        collider.enabled = false;
    }
}
