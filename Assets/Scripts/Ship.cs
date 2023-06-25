using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private const float ThrustForce = 50f;
    private const float FractionForce = 0.98f;
    private const float RotateDegreesPerSecond = 180f;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _thrustDirection;
    private bool once = true;
    private GameObject fireRef;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform firepos;
    [SerializeField] private AudioSource ayyyyy;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _thrustDirection = new Vector2(1, 0);
        ayyyyy = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vel = _rigidbody2D.velocity;
        if (Mathf.Abs(vel.x) > Mathf.Epsilon || Mathf.Abs(vel.y) > Mathf.Epsilon)
        {
            vel = vel * FractionForce;
            _rigidbody2D.velocity = vel;
        }


        if (Input.GetAxis("Thrust") > 0f)
        {
            float currentAngle = transform.eulerAngles.z;
            currentAngle *= Mathf.Deg2Rad;
            _thrustDirection = new Vector2(MathF.Cos(currentAngle), MathF.Sin(currentAngle));
            _rigidbody2D.AddForce(_thrustDirection * ThrustForce, ForceMode2D.Force);
            if (once)
            {
                fireRef = Instantiate(fire, firepos.position, firepos.rotation);
                fireRef.transform.parent = transform;
                once = false;
            }
        }
        else
        {
            once = true;
            if (fireRef != null)
            {
                Animator anim = fireRef.GetComponentInChildren<Animator>();
                anim.SetBool("isFading", true);
                
                Destroy(fireRef, .2f);
            }
        }

        float rotZ = RotateDegreesPerSecond * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 rot = new Vector3(0, 0, -rotZ);
        transform.Rotate(rot);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        ayyyyy.Play();
        Instantiate(explosion, transform.position, quaternion.identity);
        transform.position = new Vector2(1000, 1000);
    }
}
