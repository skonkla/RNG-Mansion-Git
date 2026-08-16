using UnityEngine;

public class RugPicker : MonoBehaviour
{
    public GameObject rug;
    public int x;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rug.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x < 3)
        {
            rug.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
