#nullable enable
using UnityEngine;

public class Rock : MonoBehaviour
{

#nullable disable
    public Transform rockTransform;
#nullable enable

    public void Update()
    {
        if (GameManager.Instance.Score > 0)
        {
            rockTransform.position -= new Vector3(0, SpawnRocks.Instance.RockSpeedPerSecond) * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D()
    {
        if (Yeti.Instance.pivotLocation == null && Yeti.Instance.previousPivotLocation != rockTransform)
        {
            Yeti.Instance.pivotLocation = rockTransform;
            GameManager gameManager = GameManager.Instance;
            gameManager.IncrementScore();
            gameManager.UpdateScoreText();
        }
    }
}
