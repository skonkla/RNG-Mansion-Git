using UnityEngine;

public class TitlePainting : MonoBehaviour
{
    public GameObject painting1;
    public GameObject painting2;
    public GameObject painting3;
    
    void OnEnable()
    {
        painting1.SetActive(false);
        painting2.SetActive(false);
        painting3.SetActive(false);
        int x = Random.Range(1, 101);

        print(x);

        if(x == 100)
        {
            painting3.SetActive(true);
        }
        else
        {
            x = Random.Range(1, 3);

            if (x == 1)
            {
            painting1.SetActive(true);
            }
            else if(x == 2)
            {
            painting2.SetActive(true);
            }
        }

        
    }
}
