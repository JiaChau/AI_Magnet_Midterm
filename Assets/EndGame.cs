using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject winScreen; // Assign Win Panel in Inspector
    public Button mainMenuButton; // Assign Main Menu Button in Inspector

    private void Start()
    {
        winScreen.SetActive(false); // Ensure it starts hidden
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true); // Show Win Screen
            Time.timeScale = 0f; // Pause the game
        }
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene("Menu"); // Replace with your Main Menu scene name
    }
}
