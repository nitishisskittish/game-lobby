using UnityEngine;
using System.Collections;

public class player_hallway_movement : MonoBehaviour
{
    public Transform player;
    public Transform startPoint;
    public Transform endPoint;

    public float startSpeed = 20f;
    public float endSpeed = 0.5f;
    public float moveDuration = 5f;
    public Camera vrCamera;
    public float startFOV = 130f;
    public float endFOV = 90f;
    void Start()
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

            float smoothNormalized = Mathf.Sin(normalized * Mathf.PI * 0.5f);

            player.position = Vector3.Lerp(p1, p2, smoothNormalized);
            
            vrCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, smoothNormalized);

            yield return null;
        }
    }
}
