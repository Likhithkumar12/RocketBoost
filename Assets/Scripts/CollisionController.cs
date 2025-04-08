using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionController : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay=1f;
    [SerializeField] AudioClip FinishSound;
    [SerializeField] AudioClip CrashSound;
    [SerializeField] ParticleSystem FinishParticle;
    [SerializeField] ParticleSystem CrashParticle;
    AudioSource audioSource;
    bool iscontrolenabled=true;
    bool iscollided=true;
    private void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    private void Update()
    {
       RespondToDebugKeys();
    }
    private void RespondToDebugKeys()
    {
       if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevelLoader();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            iscollided=!iscollided;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(!iscontrolenabled || !iscollided)
        {
            return;
        }
        string tag = other.gameObject.tag;
        switch(tag)
        {
            case "Finish":
                FinishSequence();
                break;
            case "Object":
                CrashSequence();
                break;

        }
    }
   private void FinishSequence()
    {
        
        iscontrolenabled=false;
        audioSource.Stop();
        audioSource.PlayOneShot(FinishSound);
        FinishParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("NextLevelLoader", LevelLoadDelay);
    }
    private void CrashSequence()
    {
        
        iscontrolenabled=false;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSound);
        CrashParticle.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("CurrentLevelLoader", LevelLoadDelay);
    }

    void NextLevelLoader()
    {
        int currentsceneid = SceneManager.GetActiveScene().buildIndex;
        int nextsceneid=currentsceneid+1;
        if(nextsceneid==SceneManager.sceneCountInBuildSettings)
        {
            nextsceneid=0;
        }
        SceneManager.LoadScene(nextsceneid);
        return;
    }

    void  CurrentLevelLoader()
    {
        int sceneid = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneid);
        return;
    }
}
