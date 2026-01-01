using UnityEngine;
using UnityEngine.UI;
public class UIFrameAnimator : MonoBehaviour
{
    public Sprite[] frames;
    [Tooltip("Frames per second")]
    public float fps = 20f;
    private Image image;
    private int currentFrame;
    private float timer;
    private bool isPlaying;
    public float transperancy = 1f;

    void Awake()
    {
        image = GetComponent<Image>();
        if (frames != null && frames.Length > 0)
            image.sprite = frames[0];
        
        Color c = image.color;
        c.a = transperancy;
        image.color = c;

        Play();
    }

    void Update()
    {
        if (!isPlaying || frames == null || frames.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer -= 1f / fps;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Pause()
    {
        isPlaying = false;
    }
}