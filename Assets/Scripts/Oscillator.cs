using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    Vector3 endposition;
    float MovementFactor;
    
    void Start()
    {
        startingPosition = transform.position;
        endposition = startingPosition + movementVector;
    }


    void Update()
    {
        MovementFactor = Mathf.PingPong(Time.time * speed,1f);
        transform.position = Vector3.Lerp(startingPosition, endposition,MovementFactor);
    }
}
