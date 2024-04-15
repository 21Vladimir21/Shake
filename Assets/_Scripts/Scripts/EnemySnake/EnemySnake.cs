using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.EnemySnake
{
    public class EnemySnake : MonoBehaviour
    {
        [SerializeField] private Texture2D notCanEat;
        [SerializeField] private Texture2D canEat;
        [SerializeField] private Color canEatColor;
        [SerializeField] private Color notCanEatColor;

        [SerializeField] private Material originalMaterial;
        [SerializeField] private float enemyLevel;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private GameObject materialsObject;
        [SerializeField] private MeshRenderer tailMesh;


        [SerializeField] private List<Burger> burger;
        [SerializeField] private List<Fence> fence;


        private Material _clonedMaterial;

        private void Awake()
        {
            levelText.text = enemyLevel.ToString();
        }

        private void CreateCloneMaterial()
        {
            _clonedMaterial = Instantiate(originalMaterial);
            Renderer renderer = materialsObject.GetComponent<Renderer>();
            if (renderer.materials.Length >= 2)
            {
                Material[] materials = renderer.materials;
                materials[1] = _clonedMaterial;

                renderer.materials = materials;
                tailMesh.material = _clonedMaterial;
            }
        }

        public void SetParts(List<Transform> parts)
        {
            foreach (var part in parts)
            {
                part.TryGetComponent(out Burger bur);
                part.TryGetComponent(out Fence fen);
                burger.Add(bur);
                fence.Add(fen);
            }
            CreateCloneMaterial();
        }

        public void TryUpdateState(float playerLevel)
        {
            if (playerLevel >= enemyLevel)
                SetCanEat();
            else
                SetNotCanEat();
        }

        private void SetCanEat()
        {
            _clonedMaterial.mainTexture = canEat;
            backgroundImage.color = canEatColor;

            foreach (var burg in burger)
            {
                if (burg == null)
                {
                
                    continue;
                }
                burg.enabled = true;
            }

            foreach (var fen in fence)
            {
                if (fen == null)
                {
           
                    continue;
                }
                fen.enabled = false;
            }
        }

        private void SetNotCanEat()
        {
            _clonedMaterial.mainTexture = notCanEat;
            backgroundImage.color = notCanEatColor;
            foreach (var burg in burger)
            {
                if (burg == null)
                {
                    burger.Remove(burg);
                    continue;
                }
                burg.enabled = false;
            }

            foreach (var fen in fence)
            {
                if (fen == null)
                {
                    fence.Remove(fen);
                    continue;
                }
                fen.enabled = true;
            }
        }
    }
}