using UnityEngine;

public class EasyRoomFoodSpawner : MonoBehaviour
{
    public RoomManager roomManager;
    GameObject currRoom;
    Transform spawn1Trans;
    Transform spawn2Trans;
    Transform spawn3Trans;
    public TimerManager timerManager;
    int i;
    Vector3[] spawnPoints = new Vector3[3];
    int s;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (i = 0; i < roomManager.easyRooms.Length; ++i)
        {

            currRoom = roomManager.easyRooms[i];

            spawn1Trans = currRoom.transform.GetChild(1);
            spawnPoints[0] = spawn1Trans.transform.position;

            spawn2Trans = currRoom.transform.GetChild(2);
            spawnPoints[1] = spawn2Trans.transform.position;

            spawn3Trans = currRoom.transform.GetChild(3);
            spawnPoints[2] = spawn3Trans.transform.position;

            RoomManager.RandomizeVectArray(spawnPoints);

            //Debug.Log("Spawning food at room #" + i);

            s = Random.Range(0, timerManager.foodies.Length);

            Instantiate(timerManager.foodies[s], spawnPoints[0], timerManager.foodies[s].transform.rotation);
            
            s = Random.Range(0, timerManager.foodies.Length);

            Instantiate(timerManager.foodies[s], spawnPoints[1], timerManager.foodies[s].transform.rotation);
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
