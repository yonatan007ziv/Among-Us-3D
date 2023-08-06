using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;
public class MenuSettings : NetworkBehaviour
{
    public TextMeshProUGUI FPS;
    public GameObject FpsLimitDropdown;
    public Slider sensSlider;
    public TMP_InputField sensInput;
    private PlayerLook playerLook;
    private void Start()
    {
        playerLook = GetComponent<PlayerLook>();
    }
    public void changeSensitivitySlider()
    {
        playerLook.sensitivity = (int)sensSlider.value;
        sensInput.text = ((int)sensSlider.value).ToString();
    }
    public void changeSensitivityInput()
    {
        if (sensInput.text == "")
        {
            sensSlider.value = 50;
            changeSensitivitySlider();
            return;
        }
        int newSens = int.Parse(sensInput.text);
        if (newSens < 1)
            newSens = 1;
        else if (newSens > 100)
            newSens = 100;
        sensSlider.value = newSens;
        changeSensitivitySlider();
    }
    public void LeaveGame()
    {
        if (NetworkClient.localPlayer.isServer)
            NetworkManager.singleton.StopHost();
        else
            NetworkManager.singleton.StopClient();
    }
    public void changeFPSLimit(int limit)
    {
        if (limit == 0)
            Application.targetFrameRate = 30;
        else if (limit == 1)
            Application.targetFrameRate = 60;
        else if (limit == 2)
            Application.targetFrameRate = 120;
        else if (limit == 3)
            Application.targetFrameRate = 144;
        else if (limit == 4)
            Application.targetFrameRate = 165;
        else if (limit == 5)
            Application.targetFrameRate = 240;
        else if (limit == 6)
            Application.targetFrameRate = 999;
    }
    public void changeVSync(bool vsync)
    {
        QualitySettings.vSyncCount = vsync ? 1 : 0;
        FpsLimitDropdown.SetActive(!vsync);
    }
    public void showFPS(bool show)
    {
        FPS.gameObject.SetActive(show);
    }
}
