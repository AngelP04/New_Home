using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Cultive
{
    public class Semilla : PersistenceObject
    {

        public float timeToGrow, timeTodestroy, actualTime, timeNoWater, timeLastPick;
        public Sprite[] stages;
        public int actualStage;
        public Item cultive;
        public bool water;
        public bool ispicked, readyToPick;
        public string nameCultive;
        private SpriteRenderer spriteRenderer;
        private CircleCollider2D circleCollider;


        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            circleCollider = GetComponent<CircleCollider2D>();
            spriteRenderer.sprite = stages[actualStage];
        }

        public override ObjectData ToData()
        {
            float[] position = new float[2];
            position[0] = transform.position.x;
            position[1] = transform.position.y;
            ObjectData data = new()
            {
                id = id,
                position = position,
                actualstage = actualStage,
                actualTime = actualTime,
                timeNoWater = timeNoWater,
                timeOfGrow = timeToGrow,
                timeToDestroy = timeTodestroy,
                timeLastPick = timeLastPick,
                isPicked = ispicked,
                readyToPicked = readyToPick,
            };
            return data;
        }

        public override void FromData(ObjectData data)
        {
            id = data.id;
            transform.position = new Vector2(data.position[0], data.position[1]);
            actualStage = data.actualstage;
            actualTime = data.actualTime;
            timeLastPick = data.timeLastPick;
            timeNoWater = data.timeNoWater;
            timeTodestroy = data.timeToDestroy;
            timeToGrow = data.timeOfGrow;
        }

        // Update is called once per frame
        void Update()
        {
            actualTime += Time.deltaTime;
            timeNoWater += Time.deltaTime;
            circleCollider.enabled = actualStage == stages.Length - 1;
            readyToPick = actualStage == stages.Length - 1;
            if (timeNoWater >= timeTodestroy)
            {
                Destroy(gameObject);
            }
            if (actualTime >= timeToGrow)
            {
                if (actualStage < stages.Length - 1)
                {
                    actualStage++;
                    actualTime = 0;
                }
            }
            if (water)
            {
                timeNoWater = 0;
                water = false;
            }

            if (ispicked)
            {
                actualStage--;
                ispicked = false;
            }

            spriteRenderer.sprite = stages[actualStage];
        }
    }

}