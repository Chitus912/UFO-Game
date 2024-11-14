using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        muted = !muted;  // Toggle trạng thái muted
        AudioListener.pause = muted;  // Đặt AudioListener.pause theo muted
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        soundOnIcon.enabled = !muted;   // Hiển thị soundOnIcon khi không tắt tiếng
        soundOffIcon.enabled = muted;   // Hiển thị soundOffIcon khi tắt tiếng
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        PlayerPrefs.Save();  // Đảm bảo lưu vào bộ nhớ
    }
}
