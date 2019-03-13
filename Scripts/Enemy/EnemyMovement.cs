using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    bool toDes = true;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            //print(player.position);
            nav.SetDestination (player.position);
            // if (toDes) {
            //     Vector3 randomDirection = Random.insideUnitSphere * 5.0f;
            //     randomDirection += player.position;
            //     nav.SetDestination (randomDirection);
            // }
        }
        else
        {
            nav.enabled = false;
        }
    }
}
