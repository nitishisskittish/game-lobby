using UnityEngine;
using System.Collections;

public class pre_holo_player_movement : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;

    public float startSpeed = 100f;
    public float endSpeed = 1f;
    public float moveDuration = 3f;
    public Camera vrCamera;
    public float startFOV = 168.9f;
    public float endFOV = 90f;

    public Renderer black_screen;
    public float fadeDuration = 0.5f;

    public pre_click_holo_movement pre_click_holo_movement_script;

    void Awake()
    {
        player.position = startPoint.position;
        vrCamera.fieldOfView = startFOV;
    }

    void Start()
    {
        StartCoroutine(Fade_in_and_move());
    }

    IEnumerator Fade_in_and_move()
    {
        yield return null;

        float tMove = 0f;

        bool hasStartedHolo = false;

        while (tMove < moveDuration)
        {
            tMove += Time.deltaTime;
            float moveNormalized = Mathf.Clamp01(tMove / moveDuration);
            float smoothMove = 1f - Mathf.Pow(1f - moveNormalized, 3f);
            if (moveNormalized >= 0.25f && !hasStartedHolo)
            {
                StartCoroutine(pre_click_holo_movement_script.move_up());
                hasStartedHolo = true;
            }
            player.position = Vector3.Lerp(startPoint.position, endPoint.position, smoothMove);
            vrCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, smoothMove);
            float fadeNormalized = tMove / fadeDuration;
            Color c = black_screen.material.color;
            c.a = 1f - fadeNormalized;
            black_screen.material.color = c;
            yield return null;
        }
        player.position = endPoint.position;
        vrCamera.fieldOfView = endFOV;
    }
}