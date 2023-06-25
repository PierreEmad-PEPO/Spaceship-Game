using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteriod : MonoBehaviour
{
    [SerializeField] float minImpulseForce = 30f;
    [SerializeField] float maxImpulseForce = 90f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Sprite[] _sprites = new Sprite[3];
    private float minScale = 4f;
    Direction direction;


    void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, 3)];
    }

    void Go()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(force * Random.Range(minImpulseForce, maxImpulseForce), ForceMode2D.Impulse);
    }

    void InitPosition()
    {
        int _case = Random.Range(0, 4);
        Vector2 pos = Vector2.zero;

        switch (_case)
        {
            case 0:
                pos.y = ScreenUtils.ScreenTop + 1;
                pos.x = Random.Range(ScreenUtils.ScreenLeft, ScreenUtils.ScreenRight);
                direction = Direction.Down;
                break;
            case 1:
                pos.y = ScreenUtils.ScreenBottom - 1;
                pos.x = Random.Range(ScreenUtils.ScreenLeft, ScreenUtils.ScreenRight);
                direction = Direction.Up;
                break;
            case 2:
                pos.x = ScreenUtils.ScreenLeft - 1;
                pos.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                direction = Direction.Right;
                break;
            case 3:
                pos.x = ScreenUtils.ScreenRight + 1;
                pos.y = Random.Range(ScreenUtils.ScreenBottom, ScreenUtils.ScreenTop);
                direction = Direction.Left;
                break;
        }

        transform.position = pos;
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, 3)];

        
        Vector2 force = new Vector2(0,0);
        if(direction == Direction.Up)force.y = 1f;
        else if(direction == Direction.Down)force.y = -1f;
        else if(direction == Direction.Left)force.x = -1f;
        else if(direction == Direction.Right)force.x = 1f;
        GetComponent<Rigidbody2D>().AddForce(force * Random.Range(minImpulseForce, maxImpulseForce), ForceMode2D.Impulse);

    }
    
   

    void OnBecameInvisible()
    {
        ChangeSprite();
        Go();
    }

    void Start()
    {
        Go();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spaceship")) return;
        Destroy(collision.gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if(transform.localScale.x >= minScale &&  transform.localScale.y >= minScale)
        {
            Vector3 newScale = transform.localScale;
            newScale.x /= 2;
            newScale.y /= 2;

            GameObject smallAsteriod1 = Instantiate<GameObject>(gameObject, transform.position, transform.rotation);
            smallAsteriod1.transform.localScale = newScale;
        
            GameObject smallAsteriod2 = Instantiate<GameObject>(gameObject, transform.position, transform.rotation);
            smallAsteriod2.transform.localScale = newScale;
        }

        Destroy(gameObject);

    }
}
