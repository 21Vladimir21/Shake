using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _mouthClosed = true;

    private const string OpenMouthAnimatorKey = "IsShouldOpenMouth";

    public void OpenMouth()
    {
        if (_mouthClosed)
        {
            _mouthClosed = false;
            _animator.SetBool(OpenMouthAnimatorKey, true);
        }
    }

    public void CloseMouth()
    {
        if (_mouthClosed == false)
        {
            _mouthClosed = true;
            _animator.SetBool(OpenMouthAnimatorKey, false);
        }
    }
}