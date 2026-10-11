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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "StoveA" && roomManager.upgradeAvailable == true && Input.GetKey(KeyCode.E))
        {
            upgradeManager.upBonusTime = true;
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
        else if (other.gameObject.tag == "StoveB" && roomManager.upgradeAvailable == true && Input.GetKey(KeyCode.E))
        {
            upgradeManager.upFoodHeal = true;
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
        else if (other.gameObject.tag == "StoveC" && roomManager.upgradeAvailable == true && Input.GetKey(KeyCode.E))
        {
            upgradeManager.upFoodStamina = true;
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
        else if (other.gameObject.tag == "StoveD" && roomManager.upgradeAvailable == true && Input.GetKey(KeyCode.E))
        {
            upgradeManager.upMaxStamina = true;
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
        else if (other.gameObject.tag == "StoveE" && roomManager.upgradeAvailable == true && Input.GetKey(KeyCode.E))
        {
            upgradeManager.upStaminaRecharge = true;
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
        else if (other.gameObject.tag == "Sink" && roomManager.upgradeAvailable == true && fPSController.currentHealth != fPSController.maxHealth && Input.GetKey(KeyCode.E))
        {
            FPSController.OnTakeDamage(-50);
            roomManager.upgradeAvailable = false;
            RemoveText();
            roomManager.unlocked = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
           lockedText.gameObject.SetActive(false); 
        }
    }

    void RemoveText()
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
