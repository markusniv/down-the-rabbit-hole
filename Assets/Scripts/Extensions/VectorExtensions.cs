using UnityEngine;

/// <summary>
/// Extesions for vector methods
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Transforms direction to 4-way direction
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 To4WayDirection(this Vector2 vector)
    {
        bool xIsDominant = Mathf.Abs(vector.x) >= Mathf.Abs(vector.y);
        if(xIsDominant)
        {
            return new Vector2(Mathf.Round(vector.x), 0);
        }else
        {
            return new Vector2(0, Mathf.Round(vector.y));
        }
    }

    public static Vector2 GetDirectionTo(this Vector2 from, Vector2 to) => (to - from).normalized;
    public static Vector3 GetDirectionTo(this Vector3 from, Vector3 to) => (to - from).normalized;
}