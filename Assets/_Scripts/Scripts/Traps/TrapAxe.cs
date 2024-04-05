using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Traps
{
    public class TrapAxe : MonoBehaviour
    {
        [SerializeField] private Transform[] trapRoot;
        private float delayBetweenAxes = 0.1f;

        private void Start()
        {
            MoveAxes();
        }

        private void MoveAxes()
        {
            for (int i = 0; i < trapRoot.Length; i++)
            {
                MoveAxeWithDelay(trapRoot[i], i * delayBetweenAxes);
            }

        }


        private void MoveAxeWithDelay(Transform axe, float delay)
        {
            var startRotateValue = axe.eulerAngles.z;

            DOTween.Sequence().AppendInterval(delay)
                .Append(axe.DORotate(new Vector3(0, 0, -startRotateValue), 3).SetEase(Ease.InOutSine))
                .Append(axe.DORotate(new Vector3(0, 0, startRotateValue), 3).SetEase(Ease.InOutSine))
                .SetLoops(-1);
        }
    }
}