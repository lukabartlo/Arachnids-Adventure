using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _victory;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenOptions()
    {
        _options.SetActive(true);
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
    }
    public void OpenCredits()
    {
        _credits.SetActive(true);
    }

    public void CloseCredits()
    {
        _credits.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        _pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePauseMenu()
    {
        _pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenVictoryMenu()
    {
        _victory.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}