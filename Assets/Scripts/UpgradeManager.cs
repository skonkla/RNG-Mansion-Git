using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public bool upFoodStamina;
    public bool upMaxStamina;
    public bool upStaminaRecharge;
    public bool upFoodHeal;
    public bool upBonusTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upFoodStamina = false;
        upMaxStamina = false;
        upStaminaRecharge = false;
        upFoodHeal = false;
        upBonusTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
