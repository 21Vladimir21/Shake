using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class SnakeTail : MonoBehaviour
{
    [SerializeField] private Transform bonePrefab;
    [SerializeField] private Transform tailPivot;
    [Range(0, 100), SerializeField] private int startBoneCount = 4;

    private List<Transform> _snakeBones = new();

    private Vector3 _lastPosition;
    private float space = 0.37f;

    private void Start()
    {
        for (var i = 0; i < startBoneCount; i++) AddBone(i);

        _lastPosition = tailPivot.position;
    }

    private void FixedUpdate()
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

    public void ChaneTailScale(float newScale)
    {
        // foreach (var bone in _snakeBones) bone.localScale += Vector3.one * newScale * 0.01f;
        //
        // space = _snakeBones[0].localScale.x;
        BoneScale(0);
    }

    private void AddBone(int index)
    {
        var bone = Instantiate(bonePrefab, tailPivot.position + Vector3.back.normalized * space * index,
            Quaternion.identity);
        _snakeBones.Add(bone);
    }

    private void AddBone()
    {
        Vector3 previousPosition = _snakeBones[_snakeBones.Count - 1].position;
        var bone = Instantiate(bonePrefab, previousPosition + -_snakeBones[_snakeBones.Count - 1].forward * space,
            Quaternion.identity);
        // var direction = (previousPosition - bone.position).normalized;
        // var newPos = previousPosition - direction * space;
        // bone.position = newPos;

        _snakeBones.Add(bone);
    }

    int cycleNumber = 0;

    private void BoneScale(int boneNumber)
    {
        var scaleDuration = 1f;
        var seq = DOTween.Sequence();
        seq.AppendInterval(0.2f).AppendCallback(() =>
        {
            boneNumber++;
            BoneScale(boneNumber);
        }).Insert(0,
            _snakeBones[boneNumber].DOScale(Vector3.one * (space * 2), scaleDuration)).Insert(0.5f,
            _snakeBones[boneNumber].DOScale(Vector3.one * space, scaleDuration).OnComplete(() =>
            {
                cycleNumber++;
                if (cycleNumber == _snakeBones.Count)
                {
                    cycleNumber = 0;
                    AddBone();
                }
            }));
    }
}