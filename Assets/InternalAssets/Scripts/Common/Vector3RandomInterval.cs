using UnityEngine;

[System.Serializable]
public struct Vector3RandomInterval
{
    public Vector3 min, max;

    public Vector3 Next()
    {
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);

        return new Vector3 (x, y, z);
    }
}