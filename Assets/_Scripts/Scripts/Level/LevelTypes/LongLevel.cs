using UnityEngine;


public class LongLevel : MonoBehaviour, ILevel
{
    [SerializeField] private PushEnding _ending;
    public void SpawnEnding(Vector3 endingPosition)
    {
        var ending = Instantiate(_ending, endingPosition, Quaternion.identity);
        ending.transform.SetParent(transform);
        ending.Spawn(endingPosition);
    }
}
