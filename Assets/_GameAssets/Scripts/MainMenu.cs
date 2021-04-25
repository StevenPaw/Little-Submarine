using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private Slider soundVolumeSlider;
    
    private void Awake()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (soundManager != null && soundVolumeSlider != null)
        {
            soundVolumeSlider.value = soundManager.GetVolume();
            soundVolumeText.text = "VOLUME (" + ((int) (soundVolumeSlider.value * 100)).ToString() + "%)";
        }
    }

    public void ChangeVolume(float volumeIn)
    {
        soundManager.SetVolume(volumeIn);
        soundVolumeText.text = "VOLUME (" + ((int)(soundVolumeSlider.value * 100)).ToString() + "%)";
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
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
}
