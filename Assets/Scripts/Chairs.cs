using UnityEngine;

public class Chairs : MonoBehaviour
{
    public GameObject chair;
    public int x;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chair.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            chair.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
