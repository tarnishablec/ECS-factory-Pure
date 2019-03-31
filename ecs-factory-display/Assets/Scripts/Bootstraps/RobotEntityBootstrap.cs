using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class RobotEntityBootstrap : Bootstrap
{
    public GameObject robotPrefab;
    public float moveTime;
    public Mesh robotMesh;
    public Material material;

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
            entityManager.AddComponentData(robots[i], new Position { });
            entityManager.AddComponentData(robots[i], new TargetPositionComponent { Value = new float3(0, -5, 0) });
            entityManager.AddSharedComponentData(robots[i], new RenderMesh { mesh = robotMesh, material = material });
        }
        robots.Dispose();
    }
}
