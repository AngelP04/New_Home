using SuperTiled2Unity.Editor.ClipperLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    public bool flashActive;
    public float flashLenght;
    private float flashCounter;

    public GameObject damageNumber;

    private SpriteRenderer characterRenderer;

    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        if(flashActive)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter > flashLenght * 0.66f)
            {
                ToggleColor(false);
            } else if (flashCounter > flashLenght * 0.33f)
            {
                ToggleColor(true);
            } else if (flashCounter > 0)
            {
                ToggleColor(false);
            } else
            {
                ToggleColor(true);
                flashActive = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (flashLenght > 0)
        {
            flashActive = true;
            flashCounter = flashLenght;
        }
        var clone = (GameObject)Instantiate(damageNumber, transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<DamageNumber>().damagePoints = damage;
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    private void ToggleColor(bool visible)
    {
        characterRenderer.color = new Color(characterRenderer.color.r, characterRenderer.color.g, characterRenderer.color.b, (visible ? 1.0f: 0.0f));
    }
}
