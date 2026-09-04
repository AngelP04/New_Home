using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public float damageSpeed;
    public float damagePoints;

    public TextMeshProUGUI damageText;

    // Update is called once per frame
    void Update()
    {
        damageText.text = damagePoints.ToString();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + damageSpeed * Time.deltaTime, this.transform.position.z);
    }
}
