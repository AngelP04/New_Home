using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public float speedY, speedX;
    private SpriteRenderer spriteRenderer;
    public Item item;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.image;
        speedX = Random.Range(-0.01f, 0.01f);
        speedY = Random.Range(-0.01f, 0.01f);
        rb.AddForce(new Vector2(speedX, speedY));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            if(!InventoryManager.instance.full)
            {
                InventoryManager.instance.AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}
