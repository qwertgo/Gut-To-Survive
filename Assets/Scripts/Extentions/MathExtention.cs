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
}
