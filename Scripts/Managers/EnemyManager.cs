using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    
    public GameObject superGameObject;
    private GameObject childGameObject;
    


    void Start ()
    {
        InvokeRepeating ("Spawn", 5f, spawnTime);
        superGameObject = GameObject.FindGameObjectWithTag("emanager");
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        childGameObject = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        childGameObject.transform.parent = superGameObject.transform;
    }
}
