using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RoomActivator : MonoBehaviour
{
    public GameObject easyR;
    public GameObject medR;
    public GameObject harR;
    public GameObject cheR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        easyR.SetActive(true);
        medR.SetActive(true);
        harR.SetActive(true);
        cheR.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
