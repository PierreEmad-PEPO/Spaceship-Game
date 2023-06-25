using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPosition;
    [SerializeField] float bulletSpeed = 60f;

    private float totalHoldTime = 0.25f;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < totalHoldTime) elapsedTime += Time.deltaTime;

        if(elapsedTime > totalHoldTime && Input.GetButtonDown("Fire1"))
        {
            elapsedTime = 0f;

            GameObject bullet = Instantiate<GameObject>(bulletPrefab, bulletPosition.position, bulletPosition.rotation);

            float angle = transform.rotation.eulerAngles.z;
            angle = angle * Mathf.Deg2Rad;
            Vector2 curVelocity = GetComponent<Rigidbody2D>().velocity;
            Vector2 force = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            bullet.GetComponent<Rigidbody2D>().AddForce(curVelocity + force * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
