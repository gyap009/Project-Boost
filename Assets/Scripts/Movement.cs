using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        } else {
            StopThrusting();
        } 
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        } else if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        } else {
            StopRotating();
        }
    }


    void StartThrusting() {
         rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); 
            // note: vector3 refers to x,y,z (or: 0, 1, 0)
            // note: Time.deltaTime is the time since the last frame, multiply this to make frame rate independent

        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        } 
        if (!mainEngineParticles.isPlaying) {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting() {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void RotateLeft() {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying) {
            rightThrusterParticles.Play();
        } 
    }

    void RotateRight() {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying) {
            leftThrusterParticles.Play();
        } 
    }

    void StopRotating() {
        rightThrusterParticles.Stop(); 
        leftThrusterParticles.Stop();  
    }

    public void ApplyRotation(float rotationThisFrame) {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); // note: Vector3.forward means 0,0,1
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
