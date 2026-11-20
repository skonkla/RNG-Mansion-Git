using UnityEngine;

public class Pots : MonoBehaviour
{
    public GameObject pot1;
    public GameObject pot2;
    public int x;

    void OnEnable()
    {
        pot1.SetActive(false);
        pot2.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            pot1.SetActive(true);
        }
        else if(x == 2)
        {
            pot2.SetActive(true);
        }
    }
}
