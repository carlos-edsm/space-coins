using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform _background, _secondBackground;

    float _size;

    void Start()
    {
        _size = _background.GetComponent<Renderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        if (transform.position.y >= _secondBackground.position.y)
        {
            _background.position = new Vector3(_background.position.x, _secondBackground.position.y + _size, _background.position.z);
            Switch();
        }
    }

    void Switch()
    {
        Transform temp = _background;

        _background = _secondBackground;
        _secondBackground = temp;
    }
}
