using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCollisionHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerRuntimeStats _stats;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private PlayerPartsIncreaser increasere;
    [SerializeField] private SnakeTail snakeTail;


    private float _currentHealth;
    private PlayerMover _playerMover;

    private PlayerCollisionHandler _collisionHandler;
    private bool _isJumping;
    private bool _movementBlocked;
    private float _lastLevel;

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public float Health => _currentHealth;
    public float MaxHealth => _maxHealth;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public PlayerMover PlayerMover => _playerMover;

    public PlayerRollingMovement PlayerRollingMovement { get; private set; }

    private void Awake()
    {
        ServiceLocator.Player = this;
        _playerMover = GetComponent<PlayerMover>();
        _collisionHandler = GetComponent<PlayerCollisionHandler>();
        PlayerRollingMovement = GetComponent<PlayerRollingMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IEatable food))
            _collisionHandler.HandleFoodCollision(this, food);
        else if (other.gameObject.TryGetComponent(out ILine line))
            _collisionHandler.HandleLineTrigger(this, line);
        if (other.gameObject.TryGetComponent(out IObstacle obstacle))
            _collisionHandler.HandleObstacleCollision(this, obstacle);
        else if (other.gameObject.TryGetComponent(out IEnemy enemy))
            _collisionHandler.HandleEnemyTrigger(this, enemy);
        else if (other.TryGetComponent(out IZone zone))
            _collisionHandler.HandleZoneTrigger(this, zone);
        else if (other.TryGetComponent(out IAreaTrigger area))
            _collisionHandler.HandleAreaTriggerEnter(this, area);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isJumping && collision.gameObject.tag == "Floor")
        {
            UnlockMovement();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IAreaTrigger area))
        {
            _collisionHandler.HandleAreaTriggerStay(this, area);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IAreaTrigger area))
        {
            _collisionHandler.HandleAreaTriggerExit(this, area);
        }
    }

    private void Update()
    {
        Ray ray = new Ray(rayPoint.position, Vector3.forward);
        RaycastHit hit;
        // Debug.DrawRay(rayPoint.position, Vector3.forward,Color.cyan);
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.collider.gameObject.GetComponent<Apple>())
                _playerAnimator.OpenMouth();
        }
        else _playerAnimator.CloseMouth();
    }


    public void Push(float pushPower)
    {
        _playerMover.Push(pushPower);
    }

    public void Fall(float value)
    {
    }

    public void AnimatePunch(Direction direction)
    {
    }

    public void BlockMovement()
    {
        _movementBlocked = true;
    }

    public void SwitchSkin(Skin newSkin)
    {
       
    }

    public void UnlockMovement()
    {
        _movementBlocked = false;
        if (_isJumping) _isJumping = false;
    }

    public void TryApplyDamage(float damage, bool animated = true)
    {
        if (damage > 0)
        {
            if (_currentHealth >= damage)
            {
                
                ApplyDamage(damage,animated);
            }
            else
                ResetHealth(0);
        }
        else
        {
            damage *= -1;
            var difference = _maxHealth - _currentHealth;
            if (difference >= damage)
                ApplyDamage(-damage,animated);
            else
                ResetHealth(_maxHealth);
        }
    }

    public void Die()
    {
        ServiceLocator.GetEndCanvas().ShowLosePanel();
        _currentHealth = 0;
        increasere.ResetLevel();
    }

    private void ResetHealth(float value)
    {
        _currentHealth = value;
        _stats.SetHealth(value);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void ApplyDamage(float damage,bool isAnimated = true)
    {
        _currentHealth -= damage;
        _stats.ReduceHealth(damage);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        var levelDifference = _currentHealth - _lastLevel;
        snakeTail.ChaneTailScale(levelDifference, (int)levelDifference, isAnimated);
        _lastLevel = _currentHealth;
    }

    private void FixedUpdate()
    {
        if (!_movementBlocked)
            _playerMover.TryMove();

        if (Input.GetKeyDown(KeyCode.H))
            Debug.Log("Current health = " + _currentHealth);
    }

    public Tween DoMove(Vector3 target, float moveDuration)
    {
        return _playerMover.MoveTo(target, moveDuration);
    }

    public void JumpTo(Transform jumpTarget)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    public void PadJumpTo(Transform destination, bool isOnPad)
    {
        _isJumping = true;
    }

    public void GiveAdditionalPower(float value)
    {
        if (value > 0)
        {
            _currentHealth += value;
            _stats.AddHealth(value);
        }
    }

    public void FallInToPit()
    {
        BlockMovement();
        _playerMover.Rigidbody.velocity = Vector3.zero;
        transform.DORotate(new Vector3(90, 0, 0), 1f);
        Camera.Instance.StopFollowing();
        Died?.Invoke();
        FallingDie(1.5f);
        Invoke("Die", 1.5f);
    }

    public void DisableCollider()
    {
        transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }

    public void FallInToPool()
    {
        BlockMovement();
        Camera.Instance.StopFollowing();
        _playerMover.Rigidbody.velocity = Vector3.zero;
        Died?.Invoke();
        FallingDie(2f);
        Invoke("Die", 2f);
        Debug.Log("died");
    }

    private void FallingDie(float duration)
    {
        transform.DOMoveY(transform.position.y - 10f, duration);
    }
}