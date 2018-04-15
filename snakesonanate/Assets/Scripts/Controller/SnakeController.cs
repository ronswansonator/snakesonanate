using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeController : MonoBehaviour
{
    public float Speed = 5.0f;

    private Vector2 _direction = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos.ToVec3XY()).ToVec2XY();
        Vector2 myPos = transform.position.ToVec2XY();

        if (Vector2.Distance(mousePos, myPos) > .1f)
        {
            Vector2 perfectDirection = (mousePos - myPos).normalized;
            _direction = Vector2.Lerp(_direction, perfectDirection, Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        _rigidbody2D.velocity = _direction.normalized * Speed;
    }

    float GetAngle(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }

    Vector2 GetDirection(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}

/*Vector2 perfectDirection = (mousePos - myPos).normalized;
            float perfectAngle = GetAngle(perfectDirection);
            float currAngle = GetAngle(_direction);
            float angleThisFrame = Mathf.LerpAngle(currAngle * Mathf.Rad2Deg, perfectAngle * Mathf.Rad2Deg, Mathf.Clamp01(idk * Time.deltaTime));
            _direction = FromAngle(angleThisFrame).normalized;
*/

