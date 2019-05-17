using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    public Sprite number;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 256);
        GetComponent<SpriteRenderer>().sprite = number;
    }

    // Update is called once per frame
    void Update()
    {
        time += 1;
        if(time <= 8) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y / 2);
        } else if(time == 9) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        } else if(time <= 15) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y * 2);
        } else if(time <= 60) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        } else {
            Destroy(this.gameObject);
        }
    }
}
