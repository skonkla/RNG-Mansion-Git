using UnityEngine;

public class LilTable : MonoBehaviour
{
    public GameObject table;
    public int x;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        table.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            table.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
