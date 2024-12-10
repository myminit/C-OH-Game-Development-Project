using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public Button soundButton;       
    public Sprite soundOnIcon;            
    public Sprite soundOffIcon;       

    private bool isMuted ;             

    void Start()
    {
        UpdateSound();

        soundButton.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;  
        PlayerPrefs.SetInt("SoundMuted", isMuted ? 1 : 0); 
        PlayerPrefs.Save();
        UpdateSound();
    }

    private void UpdateSound()
    {
        if (isMuted)
        {
            AudioListener.volume = 0; 
            soundButton.image.sprite = soundOffIcon; 
        }
        else
        {
            AudioListener.volume = 1; 
            soundButton.image.sprite = soundOnIcon; 
        }
    }
}
