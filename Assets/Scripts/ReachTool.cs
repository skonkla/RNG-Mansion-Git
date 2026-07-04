using UnityEngine;
using TMPro;

public class ReachTool : MonoBehaviour
{
    public bool inReach = false;
    public PlayerController playerController;
    public TextMeshProUGUI lockedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockedText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" && playerController.unlocked == false)
        {
           lockedText.gameObject.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
           lockedText.gameObject.SetActive(false); 
        }
    }
}
