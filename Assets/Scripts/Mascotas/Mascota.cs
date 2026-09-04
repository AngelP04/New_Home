using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mascota : MonoBehaviour
{
    //Variables generales//
    public float speed = 1;
    public Rigidbody2D rb2D;
    private bool isMoving;
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 direction;

    public Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    //Variables para animales que atacan//
    public Transform target;
    private float targetDirectionX;
    private float targetDirectionY;
    public float timeBetweenAttacks, timeBetweenAttacksCounter, distanceToAttack;
    public bool Attacking, rangeToAttack;

    /*rangeTottack es para saber si el enemigo esta en el rango de ataque, 
     distanceToAttack es el tango de ataque*/

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeBetweenStepsCounter = timeBetweenSteps * UnityEngine.Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * UnityEngine.Random.Range(0.5f, 1.5f);
        timeBetweenAttacksCounter = timeBetweenAttacks;
    }

    void Update()
    {
        if(!Attacking)
        {
            //Posiblemente sea cambiado para que siga al jugador//
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;
                rb2D.velocity = direction;
                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeBetweenSteps;
                    rb2D.velocity = Vector2.zero;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeToMakeStep;
                    direction = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2)) * speed;
                }
            }
        }
        
        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);
        DoSomething();
    }

    private void FixedUpdate()
    {
        if (Attacking)
        {
            timeBetweenAttacksCounter -= Time.deltaTime;
            Debug.Log(timeBetweenAttacksCounter);
            if (math.abs(transform.position.x - target.transform.position.x) < distanceToAttack && math.abs(transform.position.y - target.transform.position.y) < distanceToAttack)
            {
                if (timeBetweenAttacksCounter < 0)
                {
                    rangeToAttack = true;
                    rb2D.velocity = Vector2.zero;
                    timeBetweenAttacksCounter = timeBetweenAttacks;
                }
                else
                {
                    rangeToAttack = false;
                }
            }
            else
            {
                rangeToAttack = false;
                rb2D.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            }
            if (target.transform.position.x < transform.position.x)
            {
                targetDirectionX = -1;
            }
            else
            {
                targetDirectionX = 1;
            }
            if (target.transform.position.y < transform.position.y)
            {
                targetDirectionY = -1;
            }
            else
            {
                targetDirectionY = 1;
            }
            animator.SetFloat("TargetDirectionX", targetDirectionX);
            animator.SetFloat("TargetDirectionY", targetDirectionY);
            animator.SetBool("Attacking", rangeToAttack);
        }

    }

    public virtual void DoSomething()
    {
        //Funciòn por si el animal hace algo especial diferente de atacar//
    }
}
