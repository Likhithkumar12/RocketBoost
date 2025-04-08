using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
   
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction Rotation;
   [SerializeField] float RotationSpeed=100f;
   [SerializeField] float ThrustSpeed=1000f;
   [SerializeField] AudioClip thrustSound;
   [SerializeField] ParticleSystem thrustParticle;
   [SerializeField] ParticleSystem LeftThrustParticle;
    [SerializeField] ParticleSystem RightThrustParticle;
    Rigidbody rb;
    AudioSource audioSource;

    void OnEnable()
    {
        thrust.Enable();
        Rotation.Enable();
    }
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

   
    private void FixedUpdate()
    {
        ThrustProcess();
        RotationProcess();
        

    }

    private void ThrustProcess()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
            StopThrusting();
    }

    private void StopThrusting()
    {
        thrustParticle.Stop();
        audioSource.Stop();
    }

    private void StartThrusting()
    {
        thrustParticle.Play();
        rb.AddRelativeForce(Vector3.up * ThrustSpeed * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
    }

    private void RotationProcess()
    {
        float RotationInput=Rotation.ReadValue<float>();
        if (RotationInput < 0)
        {
            ApplyRightRotationandThrust();
        }
        else if (RotationInput > 0)
        {
            ApplyLeftRotationandThrusting();
        }
        else
        {
            StopBoththrusts();
        }
    }

    private void StopBoththrusts()
    {
        RightThrustParticle.Stop();
        LeftThrustParticle.Stop();
    }

    private void ApplyLeftRotationandThrusting()
    {
        ApplyRotation(-RotationSpeed);
        if (!LeftThrustParticle.isPlaying)
        {
            RightThrustParticle.Stop();
            LeftThrustParticle.Play();
        }
    }

    private void ApplyRightRotationandThrust()
    {
        ApplyRotation(RotationSpeed);
        if (!RightThrustParticle.isPlaying)
        {
            LeftThrustParticle.Stop();
            RightThrustParticle.Play();
        }
    }

    private void ApplyRotation(float RotationStrength)
    {
        
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationStrength* Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
