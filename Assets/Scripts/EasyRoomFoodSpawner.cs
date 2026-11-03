using UnityEngine;

public class EasyRoomFoodSpawner : MonoBehaviour
{
    public RoomManager roomManager;
    Transform spawn1Trans;
    Transform spawn2Trans;
    Transform spawn3Trans;
    public TimerManager timerManager;
    Vector3[] spawnPoints = new Vector3[3];
    int s;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
            spawn1Trans = gameObject.transform.GetChild(1);
            spawnPoints[0] = spawn1Trans.transform.position;

            spawn2Trans = gameObject.transform.GetChild(2);
            spawnPoints[1] = spawn2Trans.transform.position;

            spawn3Trans = gameObject.transform.GetChild(3);
            spawnPoints[2] = spawn3Trans.transform.position;

            RoomManager.RandomizeVectArray(spawnPoints);

            //Debug.Log("Spawning food at room #" + i);

            s = Random.Range(0, timerManager.foodies.Length);

            Instantiate(timerManager.foodies[s], spawnPoints[0], timerManager.foodies[s].transform.rotation);
            
            s = Random.Range(0, timerManager.foodies.Length);

            Instantiate(timerManager.foodies[s], spawnPoints[1], timerManager.foodies[s].transform.rotation);
            
    }
}
