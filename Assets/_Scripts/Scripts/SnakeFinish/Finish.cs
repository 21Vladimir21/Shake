using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.SnakeFinish
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private FinishPipe[] finishPipes;
        private PlayerFinishHolder _player;
        private int _pipeNumber;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerFinishHolder holder))
            {
                _player = holder;
                _player.SetKinematic();

                GoToNextPipe(_pipeNumber);
            }
        }


        private void GoToNextPipe(int pipeNumber, Action callback = null)
        {
            finishPipes[pipeNumber].EnableCamera(_player.RotatePivot);
            _player.RotatePivot.LookAt(finishPipes[pipeNumber].Entry);
            _player.RotatePivot.DOMove(finishPipes[pipeNumber].Entry.position, finishPipes[pipeNumber].moveToPipeTime).SetEase(Ease.Linear)
                .OnComplete(() => finishPipes[pipeNumber].StartRotate(_player.RotatePivot, () =>
                {
                    pipeNumber++;
                    Debug.Log($"Pipe number {_pipeNumber}   {pipeNumber}");
                    if (_pipeNumber < finishPipes.Length) GoToNextPipe(pipeNumber);
                    else
                    {
                        Debug.Log("Last Pipe");
                    }
                }));
        }
    }
}