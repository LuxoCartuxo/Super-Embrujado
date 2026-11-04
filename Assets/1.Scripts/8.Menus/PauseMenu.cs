using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region REFERENCES
    [Header("UI DEL MENÚ DE PAUSA")]
    public GameObject pauseMenuPanel;

    [Header("CONFIGURACIÓN DE AUDIO")]
    public AudioMixer mainMixer;
    public Slider masterVolumeSlider;
    #endregion

    #region SETTINGS
    [Header("CONTROLES")]
    public KeyCode pauseKey = KeyCode.Escape;
    private bool isPaused = false;
    #endregion

    private void Start()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume");

            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.value = savedVolume;
            }

            SetMasterVolume(savedVolume);
        }
        else if (masterVolumeSlider != null)
        {
            SetMasterVolume(masterVolumeSlider.value);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    #region MÉTODOS DE PAUSA Y NAVEGACIÓN
    public void PauseGame()
    {
        isPaused = true;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu(string mainMenuSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
    #endregion

    #region MÉTODOS DE AUDIO
    public void SetMasterVolume(float sliderValue)
    {
        if (mainMixer == null) return;

        float volumeInDb = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;

        mainMixer.SetFloat("MasterVolume", volumeInDb);

        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
    #endregion
}