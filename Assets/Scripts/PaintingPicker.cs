using UnityEngine;

public class PaintingPicker : MonoBehaviour
{
    public GameObject painting1;
    public GameObject painting2;
    public int x;

    void Start()
    {
        painting1.SetActive(false);
        painting2.SetActive(false);
        x = Random.Range(1, x + 2);

        if (x == 1)
        {
            painting1.SetActive(true);
        }
        else if(x == 2)
        {
            painting2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
