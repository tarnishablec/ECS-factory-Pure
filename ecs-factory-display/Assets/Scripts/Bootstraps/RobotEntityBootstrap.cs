using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Rendering;
using Unity.Transforms;

public class RobotEntityBootstrap : Bootstrap
{
    //public GameObject robotPrefab;
    public float moveTime;
    public Mesh robotMesh;

    public Material PT;
    public Material ST;
    public Material SD;
    public Material NS;
    public Material UD;
    public Material ET;
    public Material CH;

    NativeArray<Entity> robots;
    EntityManager entityManager;

    EntityArchetype archetype;

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
    }

    public void SpawnAllRobots()
    {
        archetype = entityManager.CreateArchetype(typeof(Position), typeof(TargetPositionComponent), typeof(RenderMesh), typeof(RobotNameComponent));

        robots = new NativeArray<Entity>(RobotPool.robotsData.Count, Allocator.Temp);
        Debug.Log(robots.Length);
        entityManager.CreateEntity(archetype, robots);

        var robotNameList = new List<string>(RobotPool.robotsData.Keys);

        for (int i = 0; i < robots.Length; i++)
        {
            entityManager.SetComponentData(robots[i], new Position { });
            entityManager.SetComponentData(robots[i], new TargetPositionComponent { });
            entityManager.SetSharedComponentData(robots[i], new RenderMesh { mesh = robotMesh });
            entityManager.SetComponentData(robots[i], new RobotNameComponent { Value = new NativeString64(robotNameList[i]) });
        }
        robots.Dispose();
    }
}
