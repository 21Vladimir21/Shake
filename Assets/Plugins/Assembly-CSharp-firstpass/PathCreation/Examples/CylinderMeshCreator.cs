using UnityEngine;

namespace PathCreation.Examples
{
	public class CylinderMeshCreator : PathSceneTool
	{
		public float thickness;

		[Range(3f, 30f)]
		public int resolutionU;

		[Min(0f)]
		public float resolutionV;

		public Material material;

		[HideInInspector]
		[SerializeField]
		private GameObject meshHolder;

		private MeshFilter meshFilter;

		private MeshRenderer meshRenderer;

		private Mesh mesh;

		protected override void PathUpdated()
		{
		}

		private void CreateMesh()
		{
		}

		private void AssignMeshComponents()
		{
		}

		private void AssignMaterials()
		{
		}
	}
}
