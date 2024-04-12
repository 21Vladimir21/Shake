using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SnakeMesh : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	
	private float foo;
	
	private Mesh mesh;
	
	private void Awake()
	{
		mesh = new Mesh();
	}
	
	public void UpdateSnakeTail()
	{
		DOTween.To(() => foo, x => foo = x, 1, 1);
	}
}
