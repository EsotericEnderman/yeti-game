#nullable enable
using UnityEngine;

using Random = System.Random;

public class SpawnRocks : MonoBehaviour
{
    public static readonly float startingRockSpeedPerSecond = 1.25f;
    public static float rockSpeedPerSecond = startingRockSpeedPerSecond;
    public static readonly float rockAccelerationPerSecond = 0.0125f;
    public static readonly float maximumRockSpeedPerSecond = 3.25f;

    public static readonly float startingRockIntervalSeconds = 3.1f;
    public static float rockIntervalSeconds = startingRockIntervalSeconds;
    public static readonly float rockIntervalDecreasePerSecond = 0.0015f;
    public static readonly float minimumRockIntervalSeconds = 0.45f;

    public static readonly float rockXPositionRange = 1.4f;

    public static readonly float rockChance = 0.1f;
    public static readonly float breakingRockChance = 0.1f;

    private static SpawnRocks? instance;

    public static SpawnRocks? Instance
    {
        get { return instance; }
    }

    public GameObject? breakingRockPrefab;
    public GameObject? rockPrefab;
    public GameObject? unbreakableRockPrefab;

    private static int rockNumber = 0;

    private static float currentTime;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (GameManager.Score >= 1)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= rockIntervalSeconds)
            {
                Random random = new();

                double randomNumber = random.NextDouble();

                GameObject rock;

                if (randomNumber <= rockChance)
                {
                #nullable disable
                    rock = instance.rockPrefab;
                }
                else if (randomNumber <= rockChance + breakingRockChance)
                {
                    rock = instance.breakingRockPrefab;
                }
                else
                {
                    rock = instance.unbreakableRockPrefab;
                }

                GameObject rockCopy = Instantiate(rock);
                rockCopy.transform.position = new Vector2(GetRockXPosition(rockNumber), 5);
                #nullable enable

                Destroy(rockCopy, 15);

                rockNumber++;
                currentTime = 0;
            }

            rockSpeedPerSecond += rockAccelerationPerSecond * Time.deltaTime;
            rockIntervalSeconds -= rockIntervalDecreasePerSecond * Time.deltaTime;

            rockSpeedPerSecond = Mathf.Min(rockSpeedPerSecond, maximumRockSpeedPerSecond);
            rockIntervalSeconds = Mathf.Max(rockIntervalSeconds, minimumRockIntervalSeconds);
        }
    }

    private static float GetRockXPosition(int rockNumber)
    {
        return rockXPositionRange * Mathf.Sin(rockNumber * rockNumber);
    }
}
