using UnityEngine;

public class WindoorPicker : MonoBehaviour
{
    public GameObject door;
    public GameObject window;
    public int x;

    void Start()
    {
        door.SetActive(false);
        window.SetActive(false);
        x = Random.Range(1, x + 1);

        if (x == 1)
        {
            door.SetActive(true);
        }
        else if(x == 2)
        {
            window.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
