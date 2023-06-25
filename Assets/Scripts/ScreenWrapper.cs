using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private float _colliderRadius;
    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _colliderRadius = GetComponent<CircleCollider2D>().radius;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnBecameInvisible()
    {
        Vector2 newPos = new Vector2(0, 0);
        
        int lo = 1, hi = 1000, mid = 1;
        bool b = true;
        while (b)
        {
            if (lo == hi) b = false;
            mid = (lo + hi) / 2;
            newPos = _rigidbody.velocity.normalized * -mid;
            if (newPos.x + _colliderRadius < ScreenUtils.ScreenLeft || newPos.x - _colliderRadius > ScreenUtils.ScreenRight
                                                                    || newPos.y + _colliderRadius < ScreenUtils.ScreenBottom
                                                                    || newPos.y - _colliderRadius > ScreenUtils.ScreenTop)
            {
                hi = mid;
            }
            else lo = mid + 1;
        }
        newPos = _rigidbody.velocity.normalized * -mid;
        transform.position = newPos;
    }
    
}
