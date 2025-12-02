using UnityEngine;
using System.Collections;

public class pre_click_holo_movement : MonoBehaviour
{
    public Transform hologram;
    public Transform startPoint;
    public Transform endPoint;

    public float startSpeed = 100f;
    public float endSpeed = 0.5f;
    public float moveDuration = 5f;

    void Awake()
    {
        hologram.position = startPoint.position;
    }
    public IEnumerator move_up()
    {
        yield return null;

        float tMove = 0f;

        while (tMove < moveDuration)
        {
            tMove += Time.deltaTime;
            float moveNormalized = Mathf.Clamp01(tMove / moveDuration);
            float smoothMove = 1f - Mathf.Pow(1f - moveNormalized, 3f);
            hologram.position = Vector3.Lerp(startPoint.position, endPoint.position, smoothMove);
            yield return null;
        }
        hologram.position = endPoint.position;
    }
}