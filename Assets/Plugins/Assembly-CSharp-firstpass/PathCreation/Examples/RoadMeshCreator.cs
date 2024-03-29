using UnityEngine;

namespace PathCreation.Examples
{
	public class RoadMeshCreator : PathSceneTool
	{
		[Header("Road settings")]
		public float roadWidth;

		[Range(0f, 0.5f)]
		public float thickness;

		public bool flattenSurface;

		[Header("Material settings")]
		public Material roadMaterial;

		public Material undersideMaterial;

		public float textureTiling;

		[SerializeField]
		[HideInInspector]
		private GameObject meshHolder;

		private MeshFilter meshFilter;

		private MeshRenderer meshRenderer;

		private Mesh mesh;

		protected override void PathUpdated()
		{
		}

		private void CreateRoadMesh()
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
