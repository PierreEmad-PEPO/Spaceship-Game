using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    AudioSource audioSource;
    private const int MaxHealth = 100;
    [SerializeField] private int health = MaxHealth;
    HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        audioSource = gameObject.GetComponent<AudioSource>();

        float angle = Random.Range(0, 2*Mathf.PI);
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        int force = Random.Range(1, 25);
        GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        health -= 10;
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = (float) health / MaxHealth;
        GetComponent<SpriteRenderer>().color = color;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        hud.AddBounce();
    }
}
