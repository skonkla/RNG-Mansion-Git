using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public GameObject[] easyRooms;
    public GameObject[] medRooms;
    public GameObject[] hardRooms;
    public GameObject[] checkpointRooms;
    public GameObject startingRoom;
    int numOfRooms = 0;
    int i = 0;
    int e = 0;
    int m = 0;
    int h = 0;
    int c = 0;
    Transform spawnTrans;
    Vector3 spawnVector;
    public GameObject player;
    public TimerManager timerManager;
    public float easyTime;
    public float medTime;
    public float hardTime;
    public float chkptTime;
    public GameObject monster;
    public bool unlocked = true;
    public int foodNeeded;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (i = 0; i < easyRooms.Length; i++)
        {
            easyRooms[i].SetActive(false);
        }
        for (i = 0; i < medRooms.Length; i++)
        {
            medRooms[i].SetActive(false);
        }
        for (i = 0; i < hardRooms.Length; i++)
        {
            hardRooms[i].SetActive(false);
        }
        for (i = 0; i < checkpointRooms.Length; i++)
        {
            checkpointRooms[i].SetActive(false);
        }

        RandomizeArray(easyRooms);
        RandomizeArray(medRooms);
        RandomizeArray(hardRooms);
        RandomizeArray(checkpointRooms);

        //Debug.Log(easyRooms[0] + " " + easyRooms[1] + " " + easyRooms[2]);
        
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
        //Debug.Log("Collision Detected");
            if(numOfRooms < 3) {
                NewEasyRoom();
                startingRoom.SetActive(false);
            }
            else if(numOfRooms == 3) {
                NewCheckpoint();
                easyRooms[2].SetActive(false);
            }
            else if(numOfRooms < 7) {
                NewMedRoom();
                checkpointRooms[0].SetActive(false);
            }
            else if(numOfRooms == 7) {
                NewCheckpoint();
                medRooms[4].SetActive(false);
            }
            else if(numOfRooms < 11) {
                NewHardRoom();
                checkpointRooms[1].SetActive(false);
            }
            else
            {
                Debug.Log("You Win!");
            } 
    }

    void NewEasyRoom()
    {
        easyRooms[e].SetActive(true);
        spawnTrans = easyRooms[e].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        timerManager.AddTime(easyTime);
        timerManager.isPaused = false;
        
        if(e > 0){
            easyRooms[e - 1].SetActive(false);
        }
        e += 1;

        foodNeeded = 2;
        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Easy Room #" + e +" Spawned. This is room #" + numOfRooms);
    }
    void NewMedRoom()
    {
        medRooms[m].SetActive(true);
        spawnTrans = medRooms[m].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        
        timerManager.AddTime(medTime);
        timerManager.isPaused = false;

        if(m > 0){
            medRooms[m - 1].SetActive(false);
        }
        m += 1;

        foodNeeded = 3;
        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Medium Room #" + m +" Spawned. This is room #" + numOfRooms);
    }
    void NewHardRoom()
    {
        hardRooms[h].SetActive(true);
        spawnTrans = hardRooms[h].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        
        timerManager.AddTime(hardTime);
        timerManager.isPaused = false;

        if(h > 0){
            hardRooms[h - 1].SetActive(false);
        }
        h += 1;

        foodNeeded = 4;
        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Hard Room #" + h +" Spawned. This is room #" + numOfRooms);
    }
    void NewCheckpoint()
    {
        checkpointRooms[c].SetActive(true);
        spawnTrans = checkpointRooms[c].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        player.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        
        timerManager.AddTime(chkptTime);
        timerManager.isPaused = true;

        if(c > 0){
            checkpointRooms[c - 1].SetActive(false);
        }
        c += 1;

        unlocked = true;

        numOfRooms += 1;
        Debug.Log("Checkpoint #" + c +" Spawned. This is room #" + numOfRooms);
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
