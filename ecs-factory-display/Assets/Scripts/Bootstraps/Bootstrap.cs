using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Bootstrap : MonoBehaviour
{
    public static EntityManager GetEntityManager()
    {
        return World.Active.GetOrCreateManager<EntityManager>();
    }

    public static T GetBootstrap<T>() where T : Bootstrap
    {
        return GameObject.FindGameObjectWithTag("BootstrapManager").GetComponent<T>();
    }
}
