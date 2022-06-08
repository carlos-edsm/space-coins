using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Vector3 _initialPosition;

    Vector2 _screenBounds;

    float _currentPosition, _initialDirection, _direction; 

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _initialPosition = transform.position;
        _currentPosition = transform.position.x;
        
        if (Random.Range(1, 3) == 1)
        {
            _direction = 1;
            _initialDirection = 1;
        }
        else
        {
            _direction = -1;
            _initialDirection = -1;
        }

        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - 10)
            Destroy(this.gameObject);

        _currentPosition += Time.deltaTime * _direction;

        if (_initialDirection == 1)
        {
            if(_currentPosition >= _initialPosition.x + 1) 
            {
                _direction *= -1;
                _currentPosition = _initialPosition.x + 1;
            } 
            else if (_currentPosition <= _initialPosition.x - 0)
            {
                _direction *= -1; 
                _currentPosition = _initialPosition.x - 0;
            }
        }
        else
        {
            if(_currentPosition >= _initialPosition.x + 0) 
            {
                _direction *= -1;
                _currentPosition = _initialPosition.x + 0;
            } 
            else if (_currentPosition <= _initialPosition.x - 1)
            {
                _direction *= -1; 
                _currentPosition = _initialPosition.x - 1;
            }
        }

        transform.position = new Vector3(_currentPosition, transform.position.y, transform.position.z);
    }

    void spawnCoin()
    {
        GameObject newCoin = Instantiate(this.gameObject) as GameObject;
        newCoin.transform.position = new Vector2(Random.Range(-_screenBounds.x + 1, _screenBounds.x - 1), _screenBounds.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            spawnCoin();
            Destroy(this.gameObject);
        }
    }
}
