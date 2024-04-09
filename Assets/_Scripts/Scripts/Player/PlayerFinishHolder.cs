using UnityEngine;



    public class PlayerFinishHolder : MonoBehaviour
    {
        [field: SerializeField] public Transform RotatePivot { get; private set; }
        [SerializeField] private GameObject PlayerCamera;
        [SerializeField] private Rigidbody rigidbody;

        public void SetKinematic()
        {
            rigidbody.isKinematic = true;
        }
        
        
        
    }
