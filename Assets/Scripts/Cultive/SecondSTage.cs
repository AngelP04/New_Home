using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultive
{
    public class SecondSTage : MonoBehaviour
    {
        public bool water;
        public delegate void grow_up_plant(Vector3 position, Item item);
        public grow_up_plant OnChangePhase;
        private CultiveLayer cultiveLayer;
        public float timeToGrow, timeTodestroy;
        private float actualTime, timeNoWater;
        public Item nextStage;
        // Start is called before the first frame update
        void Start()
        {
            cultiveLayer = FindObjectOfType<CultiveLayer>();
            actualTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}