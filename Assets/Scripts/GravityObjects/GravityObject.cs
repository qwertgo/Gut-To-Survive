using UnityEngine;
using UnityEngine.Events;

public class GravityObject : MonoBehaviour
{
    public static float gravityAngle;
    protected enum State { idle, walk, jump, drop, dash, forcefield };
    protected State currentState = State.idle;
    protected static Vector2 gravityDirection = Vector2.down;
    protected static UnityEvent gravityChangedEvent;
    protected float timeSinceStartedDropping;

    protected float standartJumpLength = .5f;

    protected float CalculateVelocityGraph(float t, float jumpHeight = 15, float jumpLength = .5f)
    {
        return (t * t / jumpLength + -2 * t) / jumpLength * jumpHeight + jumpHeight;
    }
    protected Vector2 Rotate90Deg(Vector2 v, bool clockwise)
    {
        return clockwise ? new Vector2(-v.y, v.x) : new Vector2(v.y, -v.x);
    }

    


}
