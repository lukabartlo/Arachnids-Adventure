using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Player : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Sc_ButtonManager _buttonManager;

    private Sc_Spiders _spidersOverlapCircle;
    private bool _isDragged;
    private GameObject _spiders;

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
        if (ctxt.performed)
        {
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null || hit.collider.gameObject.layer != 6)
            {
                return;
            }

            _spiders = hit.collider.gameObject;

            if (_spiders.GetComponent<Sc_Spiders>().isPlaced)
                return;

            _spiders.GetComponent<CircleCollider2D>().enabled = false;

            _isDragged = true;
            _spiders.GetComponent<Sc_Spiders>().hasBeenClicked = true;
        }

        else if (ctxt.canceled && _spiders != null)
        {
            _spiders.GetComponent<Sc_Spiders>().Detect_Nearby_Nests();
            _spiders.GetComponent<CircleCollider2D>().enabled = true;
            _spiders = null;
            _isDragged = false;
        }
    }

    public void Escape(InputAction.CallbackContext ctxt) 
    {
        if (ctxt.performed)
        {
            _buttonManager.GetComponent<Sc_ButtonManager>().OpenPauseMenu();
        }
    }
}
