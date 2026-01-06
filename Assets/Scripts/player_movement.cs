using UnityEngine;
using System.Collections;

public class player_movement : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform stopPoint;
    public Transform endPoint;

    public float start_speed = 100f;
    public float end_speed = 1f;
    public float back_up_speed = 1f;

    public float moveDuration = 3f;
    public Camera vrCamera;
    public float startFOV = 168.9f;
    public float endFOV = 90f;

    public Renderer black_screen;
    public float fadeDuration = 0.5f;

    public hologram_movement holo_movement_script;

    void Awake()
    {
        player.position = startPoint.position;
        vrCamera.fieldOfView = startFOV;
    }

    void Start()
    {
        StartCoroutine(fade_in_and_move());
    }

    IEnumerator fade_in_and_move()
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
                StartCoroutine(holo_movement_script.move_up());
                hasStartedHolo = true;
            }
            player.position = Vector3.Lerp(startPoint.position, stopPoint.position, smoothMove);
            vrCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, smoothMove);
            float fadeNormalized = tMove / fadeDuration;
            Color c = black_screen.material.color;
            c.a = Mathf.Clamp01(1f - fadeNormalized);
            black_screen.material.color = c;
            yield return null;
        }
        player.position = stopPoint.position;
        vrCamera.fieldOfView = endFOV;
    }

    public IEnumerator move_and_fade_out()
    {
        float tMove = 0f;

        bool holo_retracted = false;

        StartCoroutine(holo_movement_script.move_down(result =>
        {
            holo_retracted = true;
        }));

        Vector3 dir = startPoint.forward;
        dir.y = 0;

        while (holo_retracted == false)
        {
            transform.position += -dir.normalized * back_up_speed * Time.deltaTime;
            tMove += Time.deltaTime;
            yield return null;
        }

        Vector3 pos_after_back_up = transform.position;

        while (tMove < moveDuration && holo_retracted == true)
        {
            tMove += Time.deltaTime;
            float moveNormalized = Mathf.Clamp01(tMove / moveDuration);
            float smoothMove = 1f - Mathf.Pow(1f - moveNormalized, 3f);
            player.position = Vector3.Lerp(pos_after_back_up, endPoint.position, smoothMove);
            vrCamera.fieldOfView = Mathf.Lerp(endFOV, startFOV, smoothMove);
            float fadeNormalized = tMove / fadeDuration;
            Color c = black_screen.material.color;
            c.a = Mathf.Clamp01(fadeNormalized);
            black_screen.material.color = c;
            yield return null;
        }
        player.position = endPoint.position;
        vrCamera.fieldOfView = startFOV;
    }

}