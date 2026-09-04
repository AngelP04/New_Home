using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SFXManager sFXManager;
    private Animator animator;
    private float movementX;
    private float movementY;
    public float lastdirectionX = 0;
    public float lastdirectionY = 0;
    public bool walking = false;
    public Vector2 lastMovement = Vector2.zero;

    public float speed;
    private float velocity;

    public static bool playerCreated;

    public string nextPlaceName;

    private const string attackingState = "Attacking";

    private bool attacking = false;
    public float attackTime;
    private float attackTimeCounter;

    public bool talking;

    // Start is called before the first frame update
    void Start()
    {
        sFXManager = FindObjectOfType<SFXManager>();
        lastMovement = new Vector2(1, 0);
        talking = false;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = speed;

        if(!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnMove(InputValue movement)
    {
        if (talking)
        {
            velocity = 0;
            return;
        }
        velocity = speed;
        Vector2 movementVector = movement.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        if (movementX != 0  || movementY != 0)
        {
            lastdirectionX = movementX;
            lastdirectionY = movementY;
            lastMovement = new Vector2(lastdirectionX, lastdirectionY);
            GameManager.instance.playerHealthCurrent -= 1;
        }
    }

    public void Spin(Vector2 pos)
    {
        if(pos.x != lastdirectionX)
        {
            lastdirectionX = pos.x;
        }
        else if(pos.y != lastdirectionY)
        {
            lastdirectionX = pos.y;
        }
        lastMovement = new Vector2(lastdirectionX, lastdirectionY);
    }

    private void OnFire(InputValue value)
    {
        if (talking)
        {
            return;
        }
        attacking = true;
        attackTimeCounter = attackTime;
        rigidbody.velocity = Vector2.zero;
        animator.SetBool(attackingState, true);

        sFXManager.playerAttack.Play();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x - lastdirectionX, transform.position.y - lastdirectionY), Color.red);
        walking = false;
        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                animator.SetBool(attackingState, false);
            }
        }
        else
        {
            if ((movementX != 0 || movementY != 0) && !talking)
            {
                walking = true;
            }
        }
        animator.SetBool("Movement", walking);
        animator.SetFloat("Horizontal", movementX);
        animator.SetFloat("Vertical", movementY);
        animator.SetFloat("LastDirectionX", lastMovement.x);
        animator.SetFloat("LastDirectionY", lastMovement.y);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            velocity = speed * 2;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementX, movementY) * velocity;
    }

}
