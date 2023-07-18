using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtention 
{
    public static float Modulo(float a, float n)
    {
        return ((a % n) + n) % n;
    }

    public static bool AreCloseTogether(float a, float b, float range)
    {
        if(b > a)
        {
            float tmp = a;
            a = b;
            b = tmp;
        }

        return a - b < range;
    }

    public static bool LayerIsInMask(int layer, LayerMask mask)
    {
        return (mask & (1 << layer)) != 0;
    }

    public static Vector2 SnapVectorToOrthogonal(Vector2 v)
    {
        Vector2[] orthogonalVectors = { new Vector2(1, 0), new Vector2(-1,0), new Vector2(0,1), new Vector2(0,-1) };
        int vectorIndex = 0;
        float savedDot = Vector2.Dot(orthogonalVectors[0], v);

        for(int i = 1; i < orthogonalVectors.Length; i++)
        {
            float tmpDot = Vector2.Dot(orthogonalVectors[i], v);
            if (tmpDot > savedDot)
            {
                vectorIndex = i;
                savedDot = tmpDot;
            }
        }

        return orthogonalVectors[vectorIndex];
        
    }
}
