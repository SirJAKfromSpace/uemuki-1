using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public float rotateSpeed = 1;
    GameObject sb;
    float initPosx;
    //public GameObject settingsPanel;
    bool isSettingsOpen = false;

    private void Start() {
        sb = GameObject.Find("ScreenBlinder");
        initPosx = sb.transform.position.x;
        LeanTween.moveX(sb, initPosx-1150, 1.5f).setEaseInOutQuad();

        GameObject.Find("GFXSlider").GetComponent<Slider>().value = PlayerPrefs.GetInt("gfx",2);
    }

    void Update() {
        transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed, Space.World);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PressPlay() {
        LeanTween.moveX(sb, initPosx, 1.5f).setEaseInOutQuad();
        Invoke("StartPlay", 1.5f);
    }
    void StartPlay() {
        SceneManager.LoadScene("TuteScene");
    }

    public void SettingsShow() {
        if (isSettingsOpen) {
            GameObject settings = GameObject.Find("SettingsPanel");
            LeanTween.moveX(settings, -900, 1).setEaseInOutQuad();
            isSettingsOpen = false;
        }
        else {
            GameObject settings = GameObject.Find("SettingsPanel");
            LeanTween.moveX(settings, 50, 1).setEaseInOutQuad();
            isSettingsOpen = true;
        }
    }

    //public void ChangeGraphics() {
    //Slider slider = GameObject.Find("GFXSlider").GetComponent<Slider>();
    //int settingNum = slider
    //}
    public void OnValueChanged(int newValue) {
        Slider slider = GameObject.Find("GFXSlider").GetComponent<Slider>();
        int settingNum = (int) slider.value;
        QualitySettings.SetQualityLevel(settingNum);
        PlayerPrefs.SetInt("gfx", settingNum);
        PlayerPrefs.Save();
    }
}
