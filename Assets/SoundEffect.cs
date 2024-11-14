using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect soundEffect { get; private set; }

    private AudioSource audioSource;
    [SerializeField] private AudioClip collectCoinClip;

    void Awake()
    {
        // Singleton Pattern
        if (soundEffect == null)
        {
            soundEffect = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo object không bị phá hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Xóa nếu có nhiều hơn một instance
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollectCoinSound()
    {
        audioSource.clip = collectCoinClip;
        audioSource.Play();
    }
}
