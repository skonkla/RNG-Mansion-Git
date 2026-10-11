using UnityEngine;

public class TutFoodPicker : MonoBehaviour
{
    public GameObject food1;
    public GameObject food2;
    public GameObject food3;
    public GameObject food4;
    public GameObject food5;
    public int x;

    void Start()
    {
        food1.SetActive(false);
        food2.SetActive(false);
        food3.SetActive(false);
        food4.SetActive(false);
        food5.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            food1.SetActive(true);
        }
        else if(x == 2)
        {
            food2.SetActive(true);
        }
        else if(x == 3)
        {
            food3.SetActive(true);
        }
        else if(x == 4)
        {
            food4.SetActive(true);
        }
        else if(x == 5)
        {
            food5.SetActive(true);
        }
    }
}
