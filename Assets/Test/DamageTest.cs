using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    private Coroutine monsterContact;
    public FPSController fPSController;

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController.OnTakeDamage(50);

            print("Starting coroutine!");

            monsterContact = StartCoroutine(MonsterContact());
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
        print("Waiting for 1 second!");
        yield return new WaitForSeconds(1.00f);

        

        while (fPSController.currentHealth > 0)
        {
            print("Damaging player!");
            FPSController.OnTakeDamage(50); 
        }
        

        monsterContact = null;
    }
}
