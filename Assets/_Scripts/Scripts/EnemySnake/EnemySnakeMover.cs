using UnityEngine;

namespace DefaultNamespace.EnemySnake
{
    public class EnemySnakeMover : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        [SerializeField] private Transform[] points;
        private Transform _currentPoint;
        private int _currentPointNumber;

        private void Start() => SetCurrentPoint();
        private void FixedUpdate() => Move();


        private void Move()
        {
            if (_currentPoint == null) return;

            Vector3 direction = (_currentPoint.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, _currentPoint.position) < 1f) SetCurrentPoint();
        }

        private void SetCurrentPoint()
        {
            if (_currentPointNumber >= points.Length)
            {
                _currentPoint = null;
                return;
            }

            _currentPoint = points[_currentPointNumber];
            _currentPointNumber++;
        }
    }
}