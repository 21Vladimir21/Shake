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

        
        [SerializeField] private Burger burger;
        [SerializeField] private Fence fence;
        
        

        private Material _clonedMaterial;

        private void Awake()
        {
            levelText.text = enemyLevel.ToString();
            _clonedMaterial = Instantiate(originalMaterial);
            Renderer renderer = materialsObject.GetComponent<Renderer>();
            if (renderer.materials.Length >= 2)
            {
                Material[] materials = renderer.materials;
                materials[1] = _clonedMaterial;

                renderer.materials = materials;
            }
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
            burger.enabled = true;
            fence.enabled = false;
        }

        private void SetNotCanEat()
        {
            _clonedMaterial.mainTexture = notCanEat;
            backgroundImage.color = notCanEatColor;
            burger.enabled = false;
            fence.enabled = true;
        }
    }
}