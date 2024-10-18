using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Victory : MonoBehaviour
{
    [SerializeField] private Sc_ButtonManager _buttonManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _buttonManager.GetComponent<Sc_ButtonManager>().OpenVictoryMenu();
    }
}
