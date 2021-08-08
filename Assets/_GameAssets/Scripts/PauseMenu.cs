using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool pauseMenuActive = false;
    [SerializeField] private Canvas pauseMenuCanvas;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private Slider soundVolumeSlider;

    [SerializeField] private PlayerController playController;

    private void Start()
    {
        playController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundVolumeSlider.value = soundManager.GetVolume();
        soundVolumeText.text = ((int)(soundVolumeSlider.value * 100)).ToString() + "%";
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (pauseMenuActive && !pauseMenuCanvas.enabled)
        {
            pauseMenuCanvas.enabled = true;
            soundVolumeSlider.enabled = true;
            playController.SetCanMove(false);
        }
        else if (!pauseMenuActive && pauseMenuCanvas.enabled)
        {
            pauseMenuCanvas.enabled = false;
            soundVolumeSlider.enabled = false;
            playController.SetCanMove(true);
        }
    }

    public void ClosePauseMenu()
    {
        pauseMenuActive = false;
    }

    public void ChangeVolume(float volumeIn)
    {
        soundManager.SetVolume(volumeIn);
        soundVolumeText.text = ((int)(soundVolumeSlider.value * 100)).ToString() + "%";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void CloseGame()
    {
        Debug.Log("GAME IS QUITTING");
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseMenuActive = !pauseMenuActive;
    }
}
