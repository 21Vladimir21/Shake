using System;
using UnityEngine;

public class Fence : MonoBehaviour, IObstacle
{
    [SerializeField] private float _fenceDamage;
    [SerializeField] private float _pushPower;
    [SerializeField] private AudioClip _hitSound;

    private void Start()
    {
    }

    public void AffectPlayer(Player player)
    {
        if (enabled== false) return;
        player.TryApplyDamage(_fenceDamage,false);
        player.Push(_pushPower);
        // SoundManager.Instance.PlaySound(_hitSound);
    }
}
