using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int upFoodStamina;
    public int upMaxStamina;
    public int upStaminaRecharge;
    public int upFoodHeal;
    public int upBonusTime;
    public int totalUpgrades;

    void Start()
    {
        upFoodStamina = 0;
        upMaxStamina = 0;
        upStaminaRecharge = 0;
        upFoodHeal = 0;
        upBonusTime = 0;
        totalUpgrades = 0;
    }
}
