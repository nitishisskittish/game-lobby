using UnityEngine;
using System.Collections;

public class player_hallway_movement : MonoBehaviour
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
    void Awake()
    {
        begin_movement();
    }
    public void begin_movement()
    {
        StartCoroutine(pre_hologram_movement());
    }

    IEnumerator pre_hologram_movement()
    {
        float t = 0f;

        Vector3 p1 = startPoint.position;
        Vector3 p2 = endPoint.position;

        while (t < moveDuration)
        {
            t += Time.deltaTime;

            float normalized = t / moveDuration;

            float speedCurve = Mathf.Lerp(startSpeed, endSpeed, normalized);

            float smoothNormalized = 1f - Mathf.Pow(1f - normalized, 3f);

            player.position = Vector3.Lerp(p1, p2, smoothNormalized);
            
            vrCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, smoothNormalized);

            yield return null;
        }
    }
}
