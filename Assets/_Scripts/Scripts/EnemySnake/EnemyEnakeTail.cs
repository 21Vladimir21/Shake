using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

namespace DefaultNamespace.EnemySnake
{
    public class EnemyEnakeTail : MonoBehaviour
    {
        [SerializeField] private SplineComputer splineComputer;
        [SerializeField] private TubeGenerator tubeGenerator;
        [SerializeField] private EnemySnake enemySnake;
        
        [SerializeField] private Transform tailPivot;
        [Range(0, 100), SerializeField] private int startBoneCount = 4;
        [SerializeField] private Transform bonePrefab;
        
        [SerializeField] private float space;
        [SerializeField] private float boneScale;
        private List<Transform> _snakeBones = new();
        private Vector3 _lastPosition;

        private void Start()
        {
            for (var i = 0; i < startBoneCount; i++) AddBone(i);

            enemySnake.SetParts(_snakeBones);
            _lastPosition = tailPivot.position;
        }

        private void LateUpdate()
        {

            splineComputer.SetPoints(_snakeBones.Select(x => new SplinePoint
            {
                position = x.position,
                normal = Vector3.up,
                size = x.localScale.x
            }).ToArray());
        }
        private void DirectionTailMoving2()
        {
            tubeGenerator.sizeModifier.keys[0].size = -(boneScale - 0.05f);
            if (Vector3.Distance(tailPivot.position, _lastPosition) > 0.01f)
            {
                for (int i = 0; i < _snakeBones.Count; i++)
                {
                    Transform tailTarget;
                    if (i == 0)
                    {
                        _snakeBones[i].position = tailPivot.position;
                        _snakeBones[i].forward = tailPivot.forward;
                        continue;
                    }
                    else tailTarget = _snakeBones[i - 1];

                    Vector3 tailPos = tailTarget.position - tailTarget.forward * space;
                    _snakeBones[i].LookAt(tailPos);

                    _snakeBones[i].position = Vector3.Lerp(_snakeBones[i].position, tailPos,
                        Time.deltaTime * 20);
                }
            }

            _lastPosition = tailPivot.position;
        }
        
        private void AddBone(int index)
        {
            var bone = Instantiate(bonePrefab, tailPivot.position + Vector3.back.normalized * space * index,
                Quaternion.identity);

            bone.localScale = Vector3.one * boneScale;

            _snakeBones.Add(bone);
        }
    }
}