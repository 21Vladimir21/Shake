using UnityEngine;

public class PlayerAnimationEventReciever : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = ServiceLocator.Player.GetComponent<PlayerAnimator>();
    }

    public void StandUp()
    {
    }

    public void SetIdle()
    {
    }
    
    public void InAir()
    {
    }

    public void UnlockMovement()
    {
    }
    
}
