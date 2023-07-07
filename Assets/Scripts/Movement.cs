using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 10f; 
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineBoosterParticle;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

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
    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space)){
           rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
           if(!audioSource.isPlaying)
           {
                audioSource.PlayOneShot(mainEngine);
           }
           if(!mainEngineBoosterParticle.isPlaying){
            mainEngineBoosterParticle.Play();
           }
        }
        else{
           audioSource.Stop();
           mainEngineBoosterParticle.Stop();
        }
    }
    void ProcessRotation(){
        if (Input.GetKey(KeyCode.A)){
            ApplyRotation(rotationThrust);
            if(!rightThrusterParticles.isPlaying){
                rightThrusterParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);
            if(!leftThrusterParticles.isPlaying){
                leftThrusterParticles.Play();
            }
        }
        else{
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation=true;  //Freezing physics system rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=false; //Un-Freezing physics system rotation so physics system can take over
    }

}
