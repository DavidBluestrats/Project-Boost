using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float secondsToRespawn;
    [SerializeField] AudioClip[] clips;
    public ParticleSystem[] particles;
    AudioSource selfAudioSource;
    bool alreadyStarted = false;
    bool collisionsStatus = true;
    void Start()
    {
        selfAudioSource = GetComponent<AudioSource>();
    }
    public void SetCollisionsStatus()
    {
        collisionsStatus = !collisionsStatus;
        if (collisionsStatus)
        {
            Debug.Log("Collisions Enabled.");
        }
        else
        {
            Debug.Log("Collisions Disabled.");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!alreadyStarted && collisionsStatus)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("You bumped into something friendly.");

                    break;
                case "Finish":
                    Debug.Log("You finished the level!");
                    StartSuccessSequence();
                    break;
                default:
                    Debug.Log("You exploded.");
                    StartCrashSequence();
                    break;
            }
        }
    }
    void StartSuccessSequence()
    {
        alreadyStarted = true;
        selfAudioSource.Stop();
        particles[2].Stop();
        selfAudioSource.PlayOneShot(clips[1]);
        particles[0].Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", secondsToRespawn);
    }
    void StartCrashSequence()
    {
        alreadyStarted = true;
        selfAudioSource.Stop();
        particles[2].Stop();
        selfAudioSource.PlayOneShot(clips[0]);
        particles[1].Play();
        gameObject.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", secondsToRespawn);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (collisionsStatus) 
        {
            switch (other.gameObject.tag)
            {
                case "Fuel":
                    Debug.Log("Refilled fuel.");
                    gameObject.GetComponent<Movement>().SetFuel(50);
                    FindObjectOfType<FuelUI>().updateFuel(gameObject.GetComponent<Movement>().GetFuel());
                    Destroy(other.gameObject);
                    break;
            } 
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
