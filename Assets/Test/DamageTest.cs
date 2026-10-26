using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageTest : MonoBehaviour
{
    private Coroutine monsterContact;
    private Coroutine monsterSlow;
    public FPSController fPSController;
    private NavMeshAgent agent;
    private float speed;
    public float slowedSpeed = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
    }

    void OnEnable()
    {
        if(agent.speed == slowedSpeed)
        {
            print("The monster is slowed while the coroutine is inactive, resuming normal speed");
            agent.speed = speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController.OnTakeDamage(50);

            print("Starting coroutine!");

            monsterContact = StartCoroutine(MonsterContact());
            monsterSlow = StartCoroutine(MonsterSlow());
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && monsterContact != null)
        {
            print("Stopping coroutine!");
            StopCoroutine(monsterContact);
            monsterContact = null;
        }
    }

    private IEnumerator MonsterContact()
    {
        print("Waiting for 2 seconds!");
        yield return new WaitForSeconds(2.00f);

        while (fPSController.currentHealth > 0)
        {
            print("Damaging player!");
            FPSController.OnTakeDamage(50);
            
            print("Waiting for 2 seconds!");
            yield return new WaitForSeconds(2.00f);
        }

        monsterContact = null;
    }

    private IEnumerator MonsterSlow()
    {
        print("Slowing it down...");
        agent.speed = slowedSpeed;

        print("Waiting for 3.5 seconds!");
        yield return new WaitForSeconds(3.50f);

        print("resuming normal speed");
        agent.speed = speed;

        monsterContact = null;
    }
}
