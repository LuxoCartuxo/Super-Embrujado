using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(string gameSceneName)
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        print("[SISTEMA] Saliendo del juego...");
        Application.Quit();
    }
}