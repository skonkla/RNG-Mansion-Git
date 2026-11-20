using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReachTool : MonoBehaviour
{
    public RoomManager roomManager;
    public TextMeshProUGUI lockedText;
    public UpgradeManager upgradeManager;
    public FPSController fPSController;
    public GameObject[] textArray;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockedText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" && roomManager.unlocked == false)
        {
           lockedText.gameObject.SetActive(true); 
        }
    }

    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
           lockedText.gameObject.SetActive(false); 
        }
    }

    public void RemoveText()
    {
        int count = textArray.Length - 1;

            while (count >= 0)
            {
                textArray[count].SetActive(false);
                count--;
            }
    }

    public void AddText()
    {
        int count = textArray.Length - 1;

            while (count >= 0)
            {
                textArray[count].SetActive(true);
                count--;
            }
    }
}
