using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Follower : MonoBehaviour
{

    private PlayerMovement player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(player.transform.position.x - player.lastdirectionX, player.transform.position.y - player.lastdirectionY);
    }
}
