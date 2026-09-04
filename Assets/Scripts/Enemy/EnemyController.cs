using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rigidbody2D;
    private bool isMoving;
    public float timeBetweenSteps, timeParalyze;
    private float timeBetweenStepsCounter, timeParalyzeCounter;
    public bool trapped, paralyzed;

    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 direction;

    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        timeParalyzeCounter = timeParalyze;
    }

    // Update is called once per frame
    void Update()
    {
        if(!trapped && !paralyzed)
        {
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;
                rigidbody2D.velocity = direction;
                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeBetweenSteps;
                    rigidbody2D.velocity = Vector2.zero;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeToMakeStep;
                    direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * speed;
                }
            }
            animator.SetFloat(horizontal, direction.x);
            animator.SetFloat(vertical, direction.y);
        }
        if(paralyzed)
        {
            timeParalyzeCounter -= Time.deltaTime;
            if(timeParalyzeCounter <= 0)
            {
                paralyzed = false;
                timeParalyzeCounter = timeParalyze;
            }
        }
    }
}
