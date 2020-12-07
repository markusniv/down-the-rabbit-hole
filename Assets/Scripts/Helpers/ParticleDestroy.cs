using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys particle effect game objects after their animation is done
/// </summary>
public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
