using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioClip cardFlip;
    [SerializeField] private AudioClip cardMisMatch;
    [SerializeField] private AudioClip cardMatch;
    [SerializeField] private AudioClip gameFinished;


    private AudioSource sfxSource;

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus.OnPlaySfx += OnPlaySfx;
    }

    private void OnDisable()
    {
        EventBus.OnPlaySfx -= OnPlaySfx;
    }

    private void OnPlaySfx(SfxType sfxType)
    {
        switch (sfxType)
        {
            case SfxType.CardFlip:
                sfxSource.PlayOneShot(cardFlip);
                break;

            case SfxType.CardMisMatch:
                sfxSource.PlayOneShot(cardMisMatch);
                break;

            case SfxType.CardMatch:
                sfxSource.PlayOneShot(cardMatch);
                break;

            case SfxType.GameFinished:
                sfxSource.PlayOneShot(gameFinished);
                break;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}

public enum SfxType
{
    CardFlip,
    CardMatch,
    CardMisMatch,
    GameFinished
}
