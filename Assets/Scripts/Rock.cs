#nullable enable
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Transform? rockTransform;

    public void Awake()
    {
        rockTransform = GetComponent<Transform>();
    }

    public void Update()
    {
        if (GameManager.Score > 0)
        {
            #nullable disable
            rockTransform.position -= new Vector3(0, SpawnRocks.rockSpeedPerSecond) * Time.deltaTime;
            #nullable enable
        }
    }

    public void OnTriggerEnter2D()
    {
        #nullable disable
        if (Yeti.Instance.pivotLocation == null && Yeti.Instance.previousPivotLocation != rockTransform)
        #nullable enable
        {
            Yeti.Instance.pivotLocation = rockTransform;

            if (Yeti.pivotAngleDegrees != null)
            {
                GameManager.Score++;

                GameManager.Instance.UpdateScoreText();
            }
        }
    }
}
