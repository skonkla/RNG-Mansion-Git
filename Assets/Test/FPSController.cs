using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;


public class FPSController : MonoBehaviour
{
    public bool CanMove = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool useStamina = true;
    [SerializeField] private bool useFootsteps = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Health Parameters")]
    [SerializeField] public float maxHealth = 100;
    public float currentHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;

    [Header("Stamina Parameters")]
    [SerializeField] public float maxStamina = 100;
    [SerializeField] private float staminaLossMultiplier = 5;
    [SerializeField] private float timeBeforeStaminaRegen = 5.0f;
    [SerializeField] private float staminaValIncrement = 1;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentStamina;
    private Coroutine regeneratingStamina;
    public static Action<float> OnStaminaChange;


    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footStepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] carpetClips = default;
    [SerializeField] private AudioClip[] tileClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;

    private float defaultYPos = 0;
    private float timer;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [Header("Reference Variables")]
    public RoomManager roomManager;
    public UpgradeManager upgradeManager;
    public GameObject flashlight;
    public UI uI;
    

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
        
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if(canJump)
                HandleJump();

            if(canUseHeadbob)
                HandleHeadBob();

            if(useStamina)
                HandleStamina();

            if(useFootsteps)
                HandleFootsteps();

            ApplyFinalMovements();

            if (Input.GetKeyDown(KeyCode.F) && flashlight.activeInHierarchy == false)
            {
                flashlight.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.F) && flashlight.activeInHierarchy == true)
            {
                flashlight.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door") && roomManager.unlocked == true)
        {
            //print("Time to beat that room: " + roomManager.timeToBeat);
            //roomManager.timeToBeat = 0f;
            roomManager.NewRoom();
            
        }

        if (other.gameObject.CompareTag("Food"))
        {
            roomManager.foodNeeded--;
            Destroy(other.gameObject);
            

            uI.foodicons[roomManager.foodNeeded].SetActive(false);

            if (upgradeManager.upFoodStamina)
            {
                currentStamina += 25;
            }

            if (upgradeManager.upFoodHeal)
            {
                OnTakeDamage(-5);
            }
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxisRaw("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if(ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleHeadBob()
    {
        if(!characterController.isGrounded) return;

        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (IsSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z
            );
        }
    }

    private void HandleStamina()
    {
        if (upgradeManager.upMaxStamina)
        {
           maxStamina = 125; 
        }

        if(IsSprinting && currentInput != Vector2.zero)
        {
            if(regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }
            currentStamina -= staminaLossMultiplier * Time.deltaTime;

            if(currentStamina < 0)
                currentStamina = 0;

            OnStaminaChange?.Invoke(currentStamina);

            if(currentStamina <= 0)
                canSprint = false;
        }

        if(!IsSprinting && currentStamina < maxStamina && regeneratingStamina == null)
        {
           regeneratingStamina = StartCoroutine(RegenerateStamina());
        }
    }

    private void HandleFootsteps()
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero) return;

        footstepTimer -= Time.deltaTime;

        if(footstepTimer <= 0)
        {
            if(Physics.Raycast(characterController.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "Footsteps/Wood":
                    footStepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "Footsteps/Tile":
                    footStepAudioSource.PlayOneShot(tileClips[Random.Range(0, tileClips.Length - 1)]);
                        break;
                    case "Footsteps/Carpet":
                    footStepAudioSource.PlayOneShot(carpetClips[Random.Range(0, carpetClips.Length - 1)]);
                        break;
                    default:
                        footStepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                
                }
            }

            footstepTimer = GetCurrentOffset;
        }
    }

    private void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        OnDamage?.Invoke(currentHealth);

        if(currentHealth <= 0)
            KillPlayer();
    }

    private void KillPlayer()
    {
        currentHealth = 0;
        CanMove = false;
        uI.GameOver();
    }

    private void ApplyFinalMovements()
    {
        if(!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (characterController.velocity.y < -1 && characterController.isGrounded)
            moveDirection.y = 0;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator RegenerateStamina()
    {
        if(currentStamina == 0)
        {
            if (upgradeManager.upStaminaRecharge)
            {
                yield return new WaitForSeconds(timeBeforeStaminaRegen / 1.5f);
            }
            else
            {
                yield return new WaitForSeconds(timeBeforeStaminaRegen);   
            }
             
        }

        if (upgradeManager.upStaminaRecharge)
        {
           yield return new WaitForSeconds(timeBeforeStaminaRegen / 3.0f); 
        }
        else
        {
            yield return new WaitForSeconds(timeBeforeStaminaRegen / 2.0f);
        }
        
        
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while(currentStamina < maxStamina)
        {
            if(currentStamina > 0)
                canSprint = true;

            currentStamina += staminaValIncrement;

            if(currentStamina > maxStamina)
                currentStamina = maxStamina;

            OnStaminaChange?.Invoke(currentStamina);

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }

}
