using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public Transform player;
    public FPSController FPSController;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (FPSController.currentHealth > 0)
            {
               navMeshAgent.SetDestination(player.position); 
            }
            else
            {
                navMeshAgent.SetDestination(gameObject.transform.position);
            }
            
        }
    }
}
