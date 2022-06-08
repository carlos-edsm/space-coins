using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    LineRenderer _lineRenderer;

    Vector3 _currentPosition, _mousePosition;

    Vector2 _screenBounds;

    int _points = 0;
    [SerializeField] TextMeshProUGUI _pointsText, _hintText;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _lineRenderer = gameObject.AddComponent<LineRenderer>();

        _lineRenderer.enabled = false;

        _lineRenderer.startWidth = .05f;
        _lineRenderer.endWidth = .05f;

        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;

        _lineRenderer.material = new Material(Shader.Find("Unlit/Texture"));
    }

    void Start()
    {
        _rigidbody2D.isKinematic = true;
    }

    void Update()
    {
        _currentPosition = _rigidbody2D.position;

        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (transform.position.x > _screenBounds.x)
        {
            transform.position = new Vector2(-_screenBounds.x, transform.position.y);
        }

        if (transform.position.x < -_screenBounds.x)
        {
            transform.position = new Vector2(_screenBounds.x, transform.position.y);
        }

        if (transform.position.y < Camera.main.transform.position.y - 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnMouseDown()
    {
        if (_rigidbody2D.isKinematic)
        {
            _lineRenderer.SetPosition(0, _currentPosition);
            _lineRenderer.SetPosition(1, _currentPosition);
            _lineRenderer.enabled = true;
        }
    }

    void OnMouseDrag()
    {
        if (_rigidbody2D.isKinematic)
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(1, _mousePosition + Vector3.forward); 
        }
    }

    void OnMouseUp()
    {
         _hintText.text = "";

        if (_rigidbody2D.isKinematic)
        {
            _lineRenderer.enabled = false;

            Vector2 direction = _currentPosition - _mousePosition;

            float force = direction.y * 325;

            if (force > 600)
                force = 600;
        
            direction.Normalize();

            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(direction * force);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Coin>() != null)
        {
            _rigidbody2D.velocity = Vector2.zero; _rigidbody2D.angularVelocity = 0f;
            _rigidbody2D.isKinematic = true;
            
            _points += 1;
            _pointsText.text = _points.ToString();
        }
    }
}
