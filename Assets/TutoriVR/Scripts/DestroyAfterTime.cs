using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds a death attribute to gameobject.
/// </summary>
public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime;
    float birth;

    /// <summary>
    /// Sets the starting time for the gameobject
    /// </summary>
    void Start()
    {
        birth = Time.time;
    }

    /// <summary>
    /// If the gameobject's lifespan has been passed, destroy it
    /// </summary>
    void Update()
    {
        if (Time.time >= birth + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
