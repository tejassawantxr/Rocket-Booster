using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 2f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;

  AudioSource audioSource;

  void Start() {
    audioSource = GetComponent<AudioSource>();  
  }
  void OnCollisionEnter(Collision other) {
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
    audioSource.PlayOneShot(success);
    //todo add particle effect upon going to next level
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void StartCrashSequence(){
    audioSource.PlayOneShot(crash);
    //todo add particle effect upon crash
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
