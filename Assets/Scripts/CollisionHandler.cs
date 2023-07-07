using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 2f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;
  [SerializeField] ParticleSystem successParticle;
  [SerializeField] ParticleSystem crashParticle;

  AudioSource audioSource;

  bool isTransitioning=false;

  void Start() {
    audioSource = GetComponent<AudioSource>();  
  }
  void OnCollisionEnter(Collision other) {

    if(isTransitioning == true){return;}

    switch(other.gameObject.tag)
    {
        case "Friendly":
        Debug.Log("This thing is friendly");
        break;

        case "Finish":
        StartSuccessSequence();
        break;

        case "Fuel":
        Debug.Log("You picked up fuel");
        break;

        default:
        StartCrashSequence();
        break;
    }
  }

  void StartSuccessSequence(){
    isTransitioning=true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    successParticle.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void StartCrashSequence(){
    isTransitioning=true;
    audioSource.Stop();
    audioSource.PlayOneShot(crash);
    crashParticle.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }

  void ReloadLevel(){
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel(){
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
        nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
}
