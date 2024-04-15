using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;


public class SnakeTail : MonoBehaviour
{
    [SerializeField] private Transform bonePrefab;
    [SerializeField] private Transform tailPivot;
    [SerializeField] private SplineComputer splineComputer;
    [SerializeField] private TubeGenerator tubeGenerator;

    [SerializeField] private float space;
    [SerializeField] private float boneScale;
    [Range(0, 100), SerializeField] private int startBoneCount = 4;

    private List<Transform> _snakeBones = new();


    private Vector3 _lastPosition;

    private void Start()
    {
        for (var i = 0; i < startBoneCount; i++) AddBone(i);

        _lastPosition = tailPivot.position;
    }

    private void LateUpdate()
    {
        DirectionTailMoving2();

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

    private void DirectionTailMoving()
    {
        if (Vector3.Distance(tailPivot.position, _lastPosition) > 0.01f)
        {
            for (int i = 0; i < _snakeBones.Count; i++)
            {
                Vector3 previousPosition;
                if (i == 0) previousPosition = tailPivot.position;
                else previousPosition = _snakeBones[i - 1].position;
                var direction = (previousPosition - _snakeBones[i].position).normalized;
                var newPos = previousPosition - direction * (i == 0 ? 0 : space);
                _snakeBones[i].position = newPos;
            }
        }

        _lastPosition = tailPivot.position;
    }


    public void ChaneTailScale(float newScale, int newIndex, bool isAnimated)
    {
        _tailIndex = newIndex;
        foreach (var bone in _snakeBones) bone.localScale += Vector3.one * newScale * 0.015f;
        boneScale = _snakeBones[0].localScale.x;
        if (isAnimated) BoneScale(0);
        else
        {
            for (int i = 0; i < _tailIndex; i++)
            {
                AddBone();
            }
        }
    }

    private void AddBone(int index)
    {
        var bone = Instantiate(bonePrefab, tailPivot.position + Vector3.back.normalized * space * index,
            Quaternion.identity);

        bone.localScale = Vector3.one * boneScale;

        _snakeBones.Add(bone);
    }

    private void AddBone()
    {
        Vector3 previousPosition = _snakeBones[_snakeBones.Count - 1].position;
        var bone = Instantiate(bonePrefab, previousPosition + -_snakeBones[_snakeBones.Count - 1].forward * space,
            Quaternion.identity);

        bone.localScale = Vector3.one * boneScale;
        // var direction = (previousPosition - bone.position).normalized;
        // var newPos = previousPosition - direction * space;
        // bone.position = newPos;

        _snakeBones.Add(bone);
    }

    int cycleNumber = 0;
    private int _tailIndex;

    private void BoneScale(int boneNumber)
    {
        var scaleDuration = 1f;
        var seq = DOTween.Sequence();
        seq.AppendInterval(0.2f).AppendCallback(() =>
        {
            boneNumber++;
            if (boneNumber == _snakeBones.Count - 1) return;
            BoneScale(boneNumber);
        }).Insert(0,
            _snakeBones[boneNumber].DOScale(Vector3.one * (boneScale * 2), scaleDuration)).Insert(0.5f,
            _snakeBones[boneNumber].DOScale(Vector3.one * boneScale, scaleDuration).OnComplete(() =>
            {
                cycleNumber++;
                if (cycleNumber == _snakeBones.Count - 1)
                {
                    cycleNumber = 0;
                    for (int i = 0; i < _tailIndex; i++)
                    {
                        AddBone();
                    }
                }
            }));
    }
}