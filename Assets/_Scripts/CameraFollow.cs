using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 10f;
    public float xOffset = 2f;
    public float yOffset = 1f;
    public Transform player;
    private void Awake()
    {
        //player = transform.Find("Player");
    }

    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x + xOffset, player.position.y + yOffset, -10);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
