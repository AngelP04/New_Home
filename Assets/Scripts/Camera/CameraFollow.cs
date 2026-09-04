using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Vector3 playerPosition;
    [SerializeField]
    private float cameraSpeed = 4.0f;

    private Camera camera;
    private Vector3 minLimits, maxLimits;

    private float halfWidth, halfHeight;

    public bool cinematic = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!cinematic)
        {
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, playerPosition, cameraSpeed * Time.deltaTime);

            float clampX = Mathf.Clamp(this.transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);
            float clampY = Mathf.Clamp(this.transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);

            this.transform.position = new Vector3(clampX, clampY, this.transform.position.z);
        }

    }

    public void ChangeLimits(BoxCollider2D newCameraLimits)
    {
        minLimits = newCameraLimits.bounds.min;
        maxLimits = newCameraLimits.bounds.max;

        camera = GetComponent<Camera>();
        halfWidth = camera.orthographicSize;
        halfHeight = halfWidth / Screen.width * Screen.height;
    }
}
