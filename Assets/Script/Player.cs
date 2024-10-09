using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private bool _isDragged;
    private GameObject _spiders;

    void Start()
    {
        
    }

    void Update()
    {
        if (_isDragged)
        {
            Vector3 position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            _spiders.transform.position = position;
        }
    }

    public void LeftClick(InputAction.CallbackContext ctxt)
    {
        RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
        if (ctxt.performed)
        {
            if (hit.collider == null)
            {
                return;
            }

            if (hit.collider.gameObject.layer == 6)
            {
                _spiders = hit.collider.gameObject;
                _isDragged = true;
            }
        }

        else if (ctxt.canceled)
        {
            _spiders = null;
            _isDragged = false;
        }
    }
}
