using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    GameObject player;
    PlayerAg playerAg;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAg = player.GetComponent<PlayerAg>();
    }

    public void UpdateMovement(float h, float v, int orientation)
    {
        bool walking = h != 0f || v != 0f;
        if (walking) Move(h, v);
        Turning(orientation);
        if (walking) Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        Vector3 temp = transform.position + movement;
        if (Mathf.Sqrt(Mathf.Pow(temp.x, 2F) + Mathf.Pow(temp.z, 2F))>20) {
            playerAg.updateReward(-0.01f);
            temp = transform.position;
        }
        playerRigidbody.MovePosition(temp);
    }

    void Turning(int orientation)
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Quaternion newRotation = Quaternion.Euler(0, (float)orientation, 0);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.transform.name != "Floor") { }
            //print("[OnCollisionStay] collisionInfo= " + collisionInfo.transform.name);
    }
}
