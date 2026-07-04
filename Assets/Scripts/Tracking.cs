using UnityEngine;

public class Tracking : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.LookAt(target);
    }
}
