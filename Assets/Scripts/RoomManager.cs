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
    int i = 0;
    int roomIndex;
    int checkpointIndex;
    private int lastRandomNumber;
    
    [Header("Bonus Times")]
    public float easyTime;
    public float medTime;
    public float hardTime;
    public float chkptTime;
    
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

        damageTest.speed += ((numOfRooms) * 2) / 100f;
        damageTest.agent.speed = damageTest.speed;
        print("Monster speed is now " + damageTest.agent.speed);

        //playerAudioSource.PlayOneShot(doorCreaks[Random.Range(0, doorCreaks.Length - 1)]);

        transition.FadeToBlack();

        if((numOfRooms + 1) % 5 == 0)
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
        
            timerManager.AddTime(chkptTime);
            timerManager.isPaused = true;

            foodNeeded = 1;
            unlocked = false;

            numOfRooms += 1;
            Debug.Log(checkpointRooms[checkpointIndex].name + " Spawned. This is room #" + numOfRooms);

        }
        else{

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
            timerManager.AddTime(easyTime);
            foodNeeded = 2;

        }
        else if (foodRooms[roomIndex].CompareTag("Medium"))
        {
            timerManager.AddTime(medTime);
            foodNeeded = 3;
        }
        else if (foodRooms[roomIndex].CompareTag("Hard"))
        {
            timerManager.AddTime(hardTime);
            foodNeeded = 4;
        }

        timerManager.isPaused = false;
        unlocked = false;

        for (i = 0; i < foodNeeded; i++)
        {
            uI.foodicons[i].SetActive(true);
        }

        numOfRooms += 1;
        Debug.Log(foodRooms[roomIndex].name + " Spawned. This is room #" + numOfRooms);

        }
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

    void Win()
    {
        endRoom.SetActive(true);
        endScreen.SetActive(true);
        spawnTrans = endRoom.transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(6f, 0f, 0f);
        //hardRooms[h - 1].SetActive(false);
        fPSController.flashlight.SetActive(false);
        uI.hud.SetActive(false);
        uI.gameOver = true;
        fPSController.CanMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timerManager.BestTime(timeToBeat);
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
