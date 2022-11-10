using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for starting and ending the game
/// </summary>
public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime;
    float birth;

    /// <summary>
    /// Sets the starting time for the game
    /// </summary>
    void Start()
    {
        birth = Time.time;
    }

    /// <summary>
    /// If the current time is larger than the whole lifetime of the game, destroy it
    /// </summary>
    void Update()
    {
        if (Time.time >= birth + lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
