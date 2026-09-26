using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;
    [SerializeField] private TextMeshProUGUI staminaText = default;
    [SerializeField] private TextMeshProUGUI foodText = default;

    private void OnEnable()
    {
        FPSController.OnDamage += UpdateHealth;
        FPSController.OnHeal += UpdateHealth;
        FPSController.OnStaminaChange += UpdateStamina;
        FPSController.OnEat += UpdateFood;
    }

    private void OnDisable()
    {
        FPSController.OnDamage -= UpdateHealth;
        FPSController.OnHeal -= UpdateHealth;
        FPSController.OnStaminaChange -= UpdateStamina;
        FPSController.OnEat -= UpdateFood;
    }

    private void Start()
    {
        UpdateHealth(100);
        UpdateStamina(100);
        UpdateFood(0);
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
    }

    private void UpdateStamina(float currentStamina)
    {
        staminaText.text = currentStamina.ToString("00");
    }

    private void UpdateFood(int currentFood)
    {
        foodText.text = currentFood.ToString();
    }


}
