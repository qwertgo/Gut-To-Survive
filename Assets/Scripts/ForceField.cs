using UnityEngine;
using static PolarityExtention;


public class ForceField : MonoBehaviour
{
    public Polarity polarity = Polarity.negativ;

    [SerializeField] float radius;
    [SerializeField] float outerPullStrength;
    [SerializeField] float innterPullStrength;
    [SerializeField] float pushStrength;
    [SerializeField] float rotationStrength;
    [HideInInspector] public Vector3 position;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CircleCollider2D coll;

    bool clockwiseRotation;


    private void OnDrawGizmos()
    {
        switch (polarity)
        {
            case Polarity.negativ:
                Gizmos.color = Color.blue;
                break;
            case Polarity.neutral:
                Gizmos.color = Color.green;
                break;
            default:
                Gizmos.color = Color.red;
                break;
        }

        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        float doubleRadius = radius * 2;
        transform.localScale = new Vector3(doubleRadius, doubleRadius, doubleRadius);

        position = transform.position;

        //spriteRenderer.color = polarity == Polarity.negativ ? new Color(.5f, .5f, 1) : new Color(1, .5f, .5f);
    }

    public Vector2 CalculatePlayerVelocity(Vector2 forcefieldVelocity, Polarity playerPolarity, Vector3 playerPosition, float forcefieldExitMagnitude)
    {
        if (polarity == playerPolarity)
        {
            if (forcefieldExitMagnitude > 0)
            {
                //Vector from forcefield to player
                Vector2 tmpVelocity = playerPosition - transform.position;
                tmpVelocity.Normalize();
                forcefieldVelocity = tmpVelocity * pushStrength;
            }
            else if (forcefieldVelocity.magnitude > 0)
                forcefieldVelocity += forcefieldVelocity.normalized * pushStrength;
            else
                forcefieldVelocity = (playerPosition - position).normalized;
        }
        else
        {
            //Vector from player to forcefield
            forcefieldVelocity = transform.position - playerPosition;
            float forceFieldStrength = forcefieldVelocity.magnitude / radius;         //Get How Close player is to center
            forcefieldVelocity.Normalize();


            Vector2 rotationVelocity = Rotate90Deg(forcefieldVelocity, clockwiseRotation) * rotationStrength;

            forcefieldVelocity *= Mathf.Lerp(innterPullStrength, outerPullStrength, forceFieldStrength);
            forcefieldVelocity += rotationVelocity;
        }

        return forcefieldVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj.tag.Equals("Player"))
            return;

        coll.radius = .56f;

        Vector2 playerToForcefieldVector = position - obj.transform.position;
        float forcefieldAngle = Vector2.Angle(Vector2.up, playerToForcefieldVector);


        Rigidbody2D playerRb = obj.GetComponent<Rigidbody2D>();
        Vector2 playerVelocity = playerRb.velocity;
        float playerAngle = Vector2.Angle(Vector2.up, playerVelocity);

        if(playerToForcefieldVector.x > 0)
        {
            clockwiseRotation = forcefieldAngle > playerAngle;
        }
        else
        {
            clockwiseRotation = forcefieldAngle < playerAngle;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            coll.radius = .5f;
    }

    Vector2 Rotate90Deg(Vector2 v, bool clockwise)
    {
        return clockwise ? new Vector2(-v.y, v.x) : new Vector2(v.y, -v.x);
    }
}
