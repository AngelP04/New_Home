using GameInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultive
{
    public class PlantaRecogible : MonoBehaviour
    {
        public bool isPicked;
        private CultiveLayer cultiveLayer;
        private MouseUser mouseUser;
        public Item cultive, secondStage;
        // Start is called before the first frame update
        void Start()
        {
            cultiveLayer = FindObjectOfType<CultiveLayer>();
            mouseUser = FindObjectOfType<MouseUser>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!cultiveLayer.IsEmpty(mouseUser.MouseInWorldPosition))
                {
                    if (!isPicked)
                    {
                        isPicked = true;
                    }
                }
            }
        }
    }
}