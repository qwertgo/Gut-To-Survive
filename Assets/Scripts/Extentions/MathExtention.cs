using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtention 
{
    public static float Modulo(float a, float n)
    {
        return ((a % n) + n) % n;
    }
}
