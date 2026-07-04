using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 32.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * Time.deltaTime * speed, Space.World);
    }
}
