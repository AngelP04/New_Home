using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rigidbody;

    public bool isWalking, isTalking, click, inMenu;

    public float walkTime = 1f;
    private float walkCounter;

    public float waitTime = 2.0f;
    private float waitCounter;

    private int lastDirection;
    public bool haveAction;

    private Vector2[] walkingDirection =
    {
        new Vector2(1,0), 
        new Vector2(-1,0), 
        new Vector2(0,1), 
        new Vector2 (0,-1)
    };

    public BoxCollider2D villagerZone;

    private int currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        walkCounter = walkTime;
        waitCounter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.instance.dialogActive)
        {
            click = false;
        }
        if (!DialogManager.instance.dialogActive && !inMenu)
        {
            isTalking = false;
        }
        if(isTalking)
        {
            StopWalking();
            return;

        }
        if(isWalking)
        {
            if(villagerZone != null)
            {
                if((this.transform.position.x < villagerZone.bounds.min.x && lastDirection == 1) 
                || (this.transform.position.x > villagerZone.bounds.max.x && lastDirection == 0)
                || (this.transform.position.y < villagerZone.bounds.min.y && lastDirection == 3)
                || (this.transform.position.y > villagerZone.bounds.max.y && lastDirection == 2))
                {
                    if(currentDirection == 1 || currentDirection == 3)
                    {
                        currentDirection--;
                    }
                    else
                    {
                        currentDirection++;
                    }
                }
            }
            rigidbody.velocity = walkingDirection[currentDirection] * speed;
            lastDirection = currentDirection;

            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
                walkCounter = walkTime;
                rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if(waitCounter < 0)
            {
                StartWalking();
                waitCounter = waitTime;
            }
        }
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !isTalking && !GameManager.instance.inInventory)
        {
            click = true;
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, 4);
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        rigidbody.velocity = Vector2.zero;
    }
}
