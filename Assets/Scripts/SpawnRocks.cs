#nullable enable
using UnityEngine;

using Random = System.Random;

public class SpawnRocks : MonoBehaviour
{
    private static SpawnRocks? instance;

    public static SpawnRocks? Instance
    {
        get { return instance; }
    }

    public static float rockSpeed = 1.25F;
    public static float rockInterval = 3.1F;
    public static readonly float rockXPositionRange = 1.4F;
    
    public GameObject? breakingRockPrefab;
    public GameObject? rockPrefab;
    public GameObject? unbreakableRockPrefab;

    private static int rockNumber = 0;

    private static float currentTime;

    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame.
    public void Update()
    {
        if (GameManager.Score >= 1)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= rockInterval)
            {
                Random random = new();

                double randomNumber = random.NextDouble();

                GameObject rock;

                if (randomNumber <= 0.1F)
                {
                #nullable disable
                    rock = instance.rockPrefab;
                }
                else if (randomNumber <= 0.2F)
                {
                    rock = instance.breakingRockPrefab;
                }
                else
                {
                    rock = instance.unbreakableRockPrefab;
                }

                GameObject rockCopy = Instantiate(rock);
                rockCopy.transform.position = new Vector2(RockXPosition(rockNumber), 5);
                #nullable enable

                Destroy(rockCopy, 15);

                rockNumber++;
                currentTime = 0;
            }

            rockSpeed += 0.0125F * Time.deltaTime;
            rockInterval -= 0.0015F * Time.deltaTime;

            rockSpeed = Mathf.Clamp(rockSpeed, 1.25F, 3.25F);
            rockInterval = Mathf.Clamp(rockInterval, 0.45F, 3.1F);
        }
    }

    private static float RockXPosition(int rockNumber)
    {
        return rockXPositionRange * Mathf.Sin(rockNumber * rockNumber);
    }
}
