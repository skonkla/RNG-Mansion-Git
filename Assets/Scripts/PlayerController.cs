using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
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
    public RoomManager roomManager;
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

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Detected");
        if (collision.gameObject.CompareTag("Door") && unlocked == true)
        {
            if(numOfRooms < 3) {
                NewEasyRoom();
                startingRoom.SetActive(false);
            }
            else if(numOfRooms == 3) {
                NewCheckpoint();
                easyRooms[2].SetActive(false);
            }
            else if(numOfRooms < 9) {
                NewMedRoom();
                checkpointRooms[0].SetActive(false);
            }
            else if(numOfRooms == 9) {
                NewCheckpoint();
                medRooms[4].SetActive(false);
            }
            else if(numOfRooms < 17) {
                NewHardRoom();
                checkpointRooms[1].SetActive(false);
            }
            else
            {
                Debug.Log("You Win!");
            } 
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            foodNeeded--;
            Destroy(other.gameObject);
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
        //player.transform.localRotation = Quaternion.Euler(1,1);

        roomManager.AddTime(easyTime);
        roomManager.isPaused = false;
        
        if(e > 0){
            easyRooms[e - 1].SetActive(false);
        }
        e += 1;

        foodNeeded = 2;
        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Easy Room Spawned. This is room #" + numOfRooms);
    }
    void NewMedRoom()
    {
        medRooms[m].SetActive(true);
        spawnTrans = medRooms[m].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        //player.transform.localRotation = Quaternion.Euler(1,1,1);
        
        roomManager.AddTime(medTime);
        roomManager.isPaused = false;

        if(m > 0){
            medRooms[m - 1].SetActive(false);
        }
        m += 1;

        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Medium Room Spawned. This is room #" + numOfRooms);
    }
    void NewHardRoom()
    {
        hardRooms[h].SetActive(true);
        spawnTrans = hardRooms[h].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        monster.transform.position = spawnVector;
        //player.transform.localRotation = Quaternion.Euler(1,1,1);
        
        roomManager.AddTime(hardTime);
        roomManager.isPaused = false;

        if(h > 0){
            hardRooms[h - 1].SetActive(false);
        }
        h += 1;

        unlocked = false;

        numOfRooms += 1;
        Debug.Log("Hard Room Spawned. This is room #" + numOfRooms);
    }
    void NewCheckpoint()
    {
        checkpointRooms[c].SetActive(true);
        spawnTrans = checkpointRooms[c].transform.GetChild(0);
        spawnVector = spawnTrans.transform.position;
        monster.SetActive(false);
        player.transform.position = spawnVector;
        //player.transform.localRotation = Quaternion.Euler(1,1,1);
        
        roomManager.AddTime(chkptTime);
        roomManager.isPaused = true;

        if(c > 0){
            checkpointRooms[c - 1].SetActive(false);
        }
        c += 1;

        unlocked = true;

        numOfRooms += 1;
        Debug.Log("Checkpoint Room Spawned. This is room #" + numOfRooms);
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
