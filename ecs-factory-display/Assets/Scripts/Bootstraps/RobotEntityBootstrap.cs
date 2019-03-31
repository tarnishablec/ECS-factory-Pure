using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;

public class RobotEntityBootstrap : Bootstrap
{
    public GameObject robotPrefab;
    public float moveTime;

    NativeArray<Entity> robots;
    EntityManager entityManager;

    private void Start()
    {
        entityManager = GetEntityManager();
        StartCoroutine(WaitRobotData());
        
    }

    IEnumerator WaitRobotData()
    {
        while (RobotPool.robotsData.Count==0)
        {
            yield return 0;
        }
        SpawnAllRobots();
        Debug.Log(RobotPool.robotsData.Count);
    }

    public void SpawnAllRobots()
    {
        robots = new NativeArray<Entity>(RobotPool.robotsData.Count, Allocator.Temp);
        Debug.Log(robots.Length);
        entityManager.Instantiate(robotPrefab, robots);

        for (int i = 0; i < robots.Length; i++)
        {
            entityManager.AddComponentData(robots[i], new TargetPositionComponent { Value = new float3(0, 0, -5) });
        }
        robots.Dispose();
    }
}
