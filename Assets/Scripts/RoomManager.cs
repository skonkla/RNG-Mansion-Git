using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [Header("Room Controls")]
    public GameObject[] easyRooms;
    public GameObject[] medRooms;
    public GameObject[] hardRooms;
    public GameObject[] checkpointRooms;
    public GameObject[] foodRooms;
    public GameObject startingRoom;
    public int numOfRooms = 0;
    public int easyRoomsNeeded;
    public int medRoomsNeeded;
    public int hardRoomsNeeded;
    public int roomsPerCheckpoint = 5;
    public int firstCheckPoint = 2;
    public int secondCheckPoint = 3;
    bool intro = true;
    int i = 0;
    int roomIndex;
    int checkpointIndex;
    private int lastRandomNumber;
    
    [Header("Bonus Times")]
    public float easyTime;
    public float medTime;
    public float hardTime;
    public float chkptTime;
    float timeToAdd;
    
    [Header("References")]
    public GameObject player;
    public TimerManager timerManager;
    public ReachTool reachTool;
    public GameObject monster;
    public GameObject endRoom;
    public GameObject endScreen;
    public GameObject playerCamera;
    public bool unlocked = true;
    public int foodNeeded;
    public bool upgradeAvailable = false;
    public float timeToBeat;
    public UI uI;
    public FPSController fPSController;
    public UpgradeManager upgradeManager;
    public DamageTest damageTest;
    public AudioSource playerAudioSource;
    public Transition transition;
    Transform spawnTrans;
    Vector3 spawnVector;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] doorCreaks = default;
    

    void Start()
    {
        for (i = 0; i < foodRooms.Length; i++)
        {
            foodRooms[i].SetActive(false);
        }
        for (i = 0; i < checkpointRooms.Length; i++)
        {
            checkpointRooms[i].SetActive(false);
        }

        endScreen.SetActive(false);
        
        i = 0;
    }

    void Update()
    {
        if (foodNeeded == 0)
        {
            unlocked = true;
        }
    }

    public void NewRoom()
    {
        if(monster.activeInHierarchy == false)
        {
            //DO something lol
        }

        damageTest.speed += (numOfRooms * 2) / 100f;
        damageTest.agent.speed = damageTest.speed;
        print("Monster speed is now " + damageTest.agent.speed);

        //playerAudioSource.PlayOneShot(doorCreaks[Random.Range(0, doorCreaks.Length - 1)]);

        transition.FadeToBlack();

        if(upgradeManager.totalUpgrades == 2)
        {
            intro = false;
        }

        if(intro && numOfRooms == firstCheckPoint || numOfRooms == firstCheckPoint + secondCheckPoint + 1)
        {
            SpawnCheckpoint();
        }
        else if(intro == false && (numOfRooms + 1) % roomsPerCheckpoint == 0)
        {
            SpawnCheckpoint();
        }
        else
        {
            SpawnFoodRoom();
        }
    }

    void SpawnCheckpoint()
    {
        if(foodRooms[roomIndex].activeInHierarchy)
            {
                foodRooms[roomIndex].SetActive(false);
            }
            
        checkpointIndex = GetUniqueRandom(checkpointRooms.Length);

        checkpointRooms[checkpointIndex].SetActive(true);
        reachTool.AddText();
        spawnTrans = checkpointRooms[checkpointIndex].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        fPSController.flashlight.SetActive(false);
        player.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        upgradeAvailable = true;
        
        timeToAdd = (chkptTime * (1 + (upgradeManager.upBonusTime * 0.4f))) / (1 + (upgradeManager.totalUpgrades / 8f));
        print("Adding " + timeToAdd + " to timer");
        timerManager.AddTime(timeToAdd);
        timerManager.isPaused = true;

        foodNeeded = 1;
        unlocked = false;

        numOfRooms += 1;
        Debug.Log(checkpointRooms[checkpointIndex].name + " Spawned. This is room #" + numOfRooms);

    }

    void SpawnFoodRoom()
    {
        if(numOfRooms > 0)
        {
            if(foodRooms[roomIndex].activeInHierarchy)
            {
                foodRooms[roomIndex].SetActive(false);
            }
            if(checkpointRooms[checkpointIndex].activeInHierarchy)
            {
                checkpointRooms[checkpointIndex].SetActive(false);
            }
        }

        roomIndex = GetUniqueRandom(foodRooms.Length);

        foodRooms[roomIndex].SetActive(true);
        spawnTrans = foodRooms[roomIndex].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        fPSController.flashlight.SetActive(true);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        if (foodRooms[roomIndex].CompareTag("Easy"))
        {
            timeToAdd = (easyTime * (1 + (upgradeManager.upBonusTime * 0.4f))) / (1 + (upgradeManager.totalUpgrades / 8f));
            foodNeeded = 2;
        }
        else if (foodRooms[roomIndex].CompareTag("Medium"))
        {
            timeToAdd = (medTime * (1 + (upgradeManager.upBonusTime * 0.4f))) / (1 + (upgradeManager.totalUpgrades / 8f));
            foodNeeded = 3;
        }
        else if (foodRooms[roomIndex].CompareTag("Hard"))
        {
            timeToAdd = (hardTime * (1 + (upgradeManager.upBonusTime * 0.4f))) / (1 + (upgradeManager.totalUpgrades / 8f));
            foodNeeded = 4;
        }

        print("Adding " + timeToAdd + " to timer");
        timerManager.AddTime(timeToAdd);

        timerManager.isPaused = false;
        unlocked = false;

        for (i = 0; i < foodNeeded; i++)
        {
            uI.foodicons[i].SetActive(true);
        }

        numOfRooms += 1;
        Debug.Log(foodRooms[roomIndex].name + " Spawned. This is room #" + numOfRooms);
    }

    public int GetUniqueRandom(int max)
    {
        int result = Random.Range(0, max);
        while (result == lastRandomNumber)
        {
            result = Random.Range(0, max);
        }
        lastRandomNumber = result;
        return result;
    }

    public static GameObject[] RandomizeArray(GameObject[] array){
        int count = array.Length;

        while (count > 1)
        {
            int i = Random.Range(0, count--);
            (array[i], array[count]) = (array[count], array[i]);
        }
    return array;

    }

    public static Vector3[] RandomizeVectArray(Vector3[] array){
        int count = array.Length;

        while (count > 1)
        {
            int i = Random.Range(0, count--);
            (array[i], array[count]) = (array[count], array[i]);
        }

    return array;

    }


}
