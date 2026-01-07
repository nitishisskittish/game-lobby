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
    public float waiting_speed = 0.1f;

    public float first_move_duration = 3f;
    public float final_move_duration = 5f;
    public Camera vrCamera;
    public float startFOV = 168.9f;
    public float endFOV = 90f;

    public Renderer black_screen;
    public float fadeDuration = 2.5f;

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

        while (tMove < first_move_duration)
        {
            tMove += Time.deltaTime;
            float moveNormalized = Mathf.Clamp01(tMove / first_move_duration);
            float smoothMove = 1f - Mathf.Pow(1f - moveNormalized, 3f);
            if (moveNormalized >= 0.25f && !hasStartedHolo)
            {
                StartCoroutine(holo_movement_script.move_up());
                hasStartedHolo = true;
            }
            player.position = Vector3.Lerp(startPoint.position, stopPoint.position, smoothMove);
            vrCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, smoothMove);
            float fadeNormalized = Mathf.Clamp01(tMove / fadeDuration);
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

        StartCoroutine(holo_movement_script.move_down());

        Vector3 dir = endPoint.forward;
        dir.y = 0;

        yield return new WaitForSeconds(1.1f);   

        while (tMove < final_move_duration)
        {
            tMove += Time.deltaTime;
            float moveNormalized = Mathf.Clamp01(tMove / final_move_duration);
            float smoothMove = Mathf.Pow(moveNormalized, 3f);
            player.position = Vector3.Lerp(player.position, endPoint.position, smoothMove);
            vrCamera.fieldOfView = Mathf.Lerp(endFOV, startFOV-70, smoothMove);
            float fadeNormalized = Mathf.Clamp01(tMove / (fadeDuration-0.6f));
            Color c = black_screen.material.color;
            c.a = Mathf.Clamp01(fadeNormalized);
            black_screen.material.color = c;
            yield return null;
        }
        player.position = endPoint.position;
        vrCamera.fieldOfView = startFOV;
    }

}