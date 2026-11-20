using UnityEngine;

public class TnC : MonoBehaviour
{
    public int x;
    void Start()
    {
        gameObject.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            gameObject.SetActive(true);
        }
        
    }
}
