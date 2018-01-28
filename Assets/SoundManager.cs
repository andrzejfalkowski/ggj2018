using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    public static SoundManager Instance = null;

    [SerializeField]
    private List<AudioSource> audioSources;
    [SerializeField]
    private List<AudioSource> importantAudioSources;

    [SerializeField]
    private List<AudioClip> clips;

    [SerializeField]
    private AudioClip machinegun;
    [SerializeField]
    private AudioClip heavyMachinegun;
    [SerializeField]
    private AudioClip zombieBite;

    [SerializeField]
    private AudioClip soldierInfected;
    [SerializeField]
    private AudioClip soldierDie;

    [SerializeField]
    private AudioClip supplyReady;
    [SerializeField]
    private AudioClip supplyDelivered;

    [SerializeField]
    private AudioClip intro;

    [SerializeField]
    private AudioClip doubleDamage;
    [SerializeField]
    private AudioClip meds;
    [SerializeField]
    private AudioClip beacon;

    [SerializeField]
    private List<AudioClip> dyingZombies;

    [SerializeField]
    private List<AudioClip> roaringZombies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SoldierShoot()
    {
        PlayAt(machinegun, false, 0.1f);
    }

    public void SoldierHeavyShoot()
    {
        PlayAt(heavyMachinegun, false, 0.3f);
    }

    public void ZombieBite()
    {
        //PlayAt(zombieBite);
    }

    public void ZombieDie()
    {
        PlayAt(dyingZombies[UnityEngine.Random.Range(0, dyingZombies.Count)], false, 0.1f);
    }

    public void SoldierInfected()
    {
        PlayAt(soldierInfected);
    }

    public void SoldierDie()
    {
        PlayAt(soldierDie);
    }

    public void SupplyReady()
    {
        PlayAt(supplyReady);
    }

    public void SupplyDelievered()
    {
        PlayAt(supplyDelivered);
    }

    public void Intro()
    {
        PlayAt(intro);
    }

    public void DoubleDamage()
    {
        PlayAt(doubleDamage, true, 0.5f);
    }

    public void Meds()
    {
        PlayAt(meds);
    }

    public void Beacon()
    {
        PlayAt(beacon);
    }

    public void Play(int index)
    {
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayAt(AudioClip clip, bool important = true, float volume = 1f)
    {
        if (important)
        {
            for (int i = 0; i < importantAudioSources.Count; i++)
            {
                if (!importantAudioSources[i].isPlaying)
                {
                    importantAudioSources[i].PlayOneShot(clip, volume);
                    return;
                }
            }
            importantAudioSources[UnityEngine.Random.Range(0, audioSources.Count)].PlayOneShot(clip, volume);
        }
        else
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].PlayOneShot(clip, volume);
                    return;
                }
            }
            audioSources[UnityEngine.Random.Range(0, audioSources.Count)].PlayOneShot(clip, volume);
        }
       
    }
}
