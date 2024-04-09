using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFx : MonoBehaviour
{
    [SerializeField] private GameObject fx;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerFinishHolder holder))
        {
            fx.SetActive(true);
        }
    }
}
