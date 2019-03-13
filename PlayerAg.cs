using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAg : Agent
{
    Rigidbody rBody;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    GameObject enemyManager;
    public bool isShoot;
    //bool inAction = false;
    //int reward;
    //private int time = 5;
    System.Diagnostics.Stopwatch stopwatch;

    void Start () {
        rBody = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        enemyManager = GameObject.FindGameObjectWithTag("emanager");

        isShoot = false;
        //reward = 0;
    }

    public Transform Target;
    public override void AgentReset()
    {
        this.transform.position = new Vector3( 0, 0, 0);
        playerHealth.currentHealth = playerHealth.startingHealth;
        playerHealth.healthSlider.value = playerHealth.currentHealth;
        isShoot = false;
        int childCount = enemyManager.transform.childCount;
        for (int i = 0; i < childCount ; i++) {
            Destroy (enemyManager.transform.GetChild (i).gameObject);
        }
        ScoreManager.score = 0;
    }

    public override void CollectObservations()
    {
        //print("CollectObservations");
        //print("x: " + this.transform.position.x + ",z= "+this.transform.position.z);
        //Agent position
        AddVectorObs(this.transform.position.x);
        AddVectorObs(this.transform.position.z);

        //Agent health
        AddVectorObs(playerHealth.currentHealth);

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //print("AgentAction");
        //print("x: " + this.transform.position.x + ",z= "+this.transform.position.z);
        //reward = 0;
        stopwatch = System.Diagnostics.Stopwatch.StartNew();
        int movement = Mathf.FloorToInt(vectorAction[0]);
        int orientation = (Mathf.FloorToInt(vectorAction[1])-1)*10;
        //int shoot = Mathf.FloorToInt(vectorAction[2]);

        //isShoot = shoot == 1 ? true : false;
        isShoot = true;

        Vector3 v = getMovement(movement);
        playerMovement.UpdateMovement(v[0], v[2], orientation);
        if(playerShooting.UpdateShoot(isShoot))
            AddReward(1f);

        //inAction = true;
        
    }

    private void FixedUpdate()
    {
        // if (inAction) {
        //     //print("SetReward " + reward);
        //     //SetReward((float) reward);
        //     //reward = 0;
        //     inAction = false;
        // }
        //Health
        if (playerHealth.currentHealth <= 0) {
            Done();
        }
    }

    public void updateReward(float score)
    {
        AddReward(score);
    }

    private Vector3 getMovement(int move) {
        switch (move) {
            case 1:
                return new Vector3(0f, 0f, 0f);
            case 2:
                return new Vector3(0f, 0f, 1f);
            case 3:
                return new Vector3(0f, 0f, -1f);
            case 4:
                return new Vector3(1f, 0f, 1f);
            case 5:
                return new Vector3(1f, 0f, 0f);
            case 6:
                return new Vector3(1f, 0f, -1f);
            case 7:
                return new Vector3(-1f, 0f, 1f);
            case 8:
                return new Vector3(-1f, 0f, 0f);
            case 9:
                return new Vector3(-1f, 0f, -1f);
            default:
                return new Vector3(0f, 0f, 0f);
        }
    }
}
