using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Player Player { get; private set; }
    public Vector3 PlayerPosition => Player.transform.position;
    public Vector3 PlayerStartPosition => transform.position;

    private void Awake()
    {
        ServiceLocator.SetSpawnPoint(this);
        Player = Instantiate(_player, transform.position, transform.rotation);
        Player.transform.SetParent(transform);
    }
}
