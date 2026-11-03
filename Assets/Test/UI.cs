using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI healthText = default;
    public TextMeshProUGUI gameOverTimerText;
    public TextMeshProUGUI finalRoomCountText;
    [Header("HUD")]
    public Image staminaBar;
    public FPSController fPSController;
    public GameObject[] foodicons;

    [Header("Other References")]
    public TimerManager timerManager;
    public RoomManager roomManager;
    public GameObject gameOverScreen;
    public GameObject hud;
    public bool gameOver = false;


    private void OnEnable()
    {
        FPSController.OnDamage += UpdateHealth;
        FPSController.OnHeal += UpdateHealth;
        FPSController.OnStaminaChange += UpdateStamina;
        
    }

    private void OnDisable()
    {
        FPSController.OnDamage -= UpdateHealth;
        FPSController.OnHeal -= UpdateHealth;
        FPSController.OnStaminaChange -= UpdateStamina;
        
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);

        UpdateHealth(100);
        UpdateStamina(100);
        
        for (int i = 0; i < foodicons.Length; i++)
        {
            foodicons[i].SetActive(false);
        }
    }

    private void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString("00");
    }

    private void UpdateStamina(float currentStamina)
    {
        staminaBar.fillAmount = currentStamina / fPSController.maxStamina;

    }

    public void GameOver()
    {
        timerManager.isPaused = true;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        hud.SetActive(false);

        gameOverTimerText.text = "You survived for " + roomManager.timeToBeat.ToString("00:00.000");

        finalRoomCountText.text = "You made it to room #" + roomManager.numOfRooms;
        
            
    }


}
