using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rotationSpeed;
    private Rigidbody selfRigidBody;
    [SerializeField] float pushForce;
    [SerializeField] float fuel = 100;
    private AudioSource selfAudioSource;
    public ParticleSystem[] boostParticles;
    public AudioClip[] clips;

    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        selfAudioSource = GetComponent<AudioSource>();
        selfAudioSource.clip = clips[0];
        transform.position = SceneMaster.active.currentCheckpoint;
    }
    public void SetFuel(float value)
    {
        fuel += value;
    }
    public float GetFuel()
    {
        return fuel;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (fuel > 0)
            {
                StartThrusting();

            }
            else
            {
                StopThrusting();
            }

        }
        else
        {
            selfAudioSource.Stop();
            boostParticles[0].Stop();
        }
    }

    private void StopThrusting()
    {
        selfAudioSource.clip = clips[1];
        if (!selfAudioSource.isPlaying)
        {
            selfAudioSource.Play();
        }
    }

    private void StartThrusting()
    {
        selfAudioSource.clip = clips[0];
        if (!selfAudioSource.isPlaying)
        {
            selfAudioSource.Play();
        }
        if (!boostParticles[0].isPlaying)
        {
            boostParticles[0].Play();
        }
        selfRigidBody.AddRelativeForce(Vector3.up * pushForce * Time.deltaTime);

        fuel -= 0.15f;
        if (fuel < 0) fuel = 0;
        FindObjectOfType<FuelUI>().updateFuel(fuel);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            boostParticles[1].Stop();
            boostParticles[2].Stop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!boostParticles[2].isPlaying)
        {
            boostParticles[2].Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!boostParticles[1].isPlaying)
        {
            boostParticles[1].Play();
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        selfRigidBody.freezeRotation = true; //Freezing rotation so we can manually rotate.
        transform.Rotate(transform.forward * Time.deltaTime * rotationThisFrame);
        selfRigidBody.freezeRotation = false; //Unfreezing rotation so the physics system can take over.
    }
}
