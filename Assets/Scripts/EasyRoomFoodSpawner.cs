using UnityEngine;

public class EasyRoomFoodSpawner : MonoBehaviour
{
    public PlayerController playerController;
    GameObject currRoom;
    Transform spawn1Trans;
    Transform spawn2Trans;
    Transform spawn3Trans;
    public RoomManager roomManager;
    int i;
    Vector3[] spawnPoints = new Vector3[3];
    int s;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (i = 0; i < playerController.easyRooms.Length; ++i)
        {

            currRoom = playerController.easyRooms[i];

            spawn1Trans = currRoom.transform.GetChild(1);
            spawnPoints[0] = spawn1Trans.transform.position;

            spawn2Trans = currRoom.transform.GetChild(2);
            spawnPoints[1] = spawn2Trans.transform.position;

            spawn3Trans = currRoom.transform.GetChild(3);
            spawnPoints[2] = spawn3Trans.transform.position;

            PlayerController.RandomizeVectArray(spawnPoints);

            //Debug.Log("Spawning food at room #" + i);

            s = Random.Range(0, roomManager.foodies.Length);

            Instantiate(roomManager.foodies[s], spawnPoints[0], roomManager.foodies[s].transform.rotation);
            
            s = Random.Range(0, roomManager.foodies.Length);

            Instantiate(roomManager.foodies[s], spawnPoints[1], roomManager.foodies[s].transform.rotation);
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
