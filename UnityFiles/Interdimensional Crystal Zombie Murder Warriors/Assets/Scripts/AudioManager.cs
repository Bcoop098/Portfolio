using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    // Use headers to create separate sections in the inspector.
    [Header("Background Music")]
    [SerializeField]
    private AudioSource backgroundAudio;

    [Header("Vessel Sound Effects")]
    [SerializeField]
    private AudioSource vesselVoiceAudio;
    [SerializeField]
    private AudioSource vesselEffectAudio;
    [SerializeField]
    private AudioClip vesselDeployed;
    [SerializeField]
    private AudioClip vesselAttacking;
    [SerializeField]
    private AudioClip vesselLowHealth;
    [SerializeField]
    private AudioClip vesselHealing;
    [SerializeField]
    private AudioClip vesselFullyHealed;

    [Header("Odo Sound Effects")]
    [SerializeField]
    private AudioSource odoVoiceAudio;
    [SerializeField]
    private AudioSource odoEffectAudio;
    [SerializeField]
    private AudioClip odoDeployed;
    [SerializeField]
    private AudioClip odoAttacking;
    [SerializeField]
    private AudioClip odoLowHealth;
    [SerializeField]
    private AudioClip odoHealing;
    [SerializeField]
    private AudioClip odoFullyHealed;

    [Header("Krux Sound Effects")]
    [SerializeField]
    private AudioSource kruxVoiceAudio;
    [SerializeField]
    private AudioSource kruxEffectAudio;
    [SerializeField]
    private AudioClip kruxDeployed;
    [SerializeField]
    private AudioClip kruxAttacking;
    [SerializeField]
    private AudioClip kruxLowHealth;
    [SerializeField]
    private AudioClip kruxHealing;
    [SerializeField]
    private AudioClip kruxFullyHealed;

    [Header("Xygo Sound Effects")]
    [SerializeField]
    private AudioSource xygoVoiceAudio;
    [SerializeField]
    private AudioSource xygoEffectAudio;
    [SerializeField]
    private AudioClip xygoDeployed;
    [SerializeField]
    private AudioClip xygoAttacking;
    [SerializeField]
    private AudioClip xygoLowHealth;
    [SerializeField]
    private AudioClip xygoHealing;
    [SerializeField]
    private AudioClip xygoFullyHealed;

    private void Start()
    {
        backgroundAudio.Play();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAttack(string _name)
    {
        switch(_name)
        {
            case "Vessel":
                ResetVesselEffectAudio();
                vesselEffectAudio.clip = vesselAttacking;
                vesselEffectAudio.Play();
                break;
            case "Odo":
                ResetOdoEffectAudio();
                odoEffectAudio.clip = odoAttacking;
                odoEffectAudio.Play();
                break;
            case "Krux":
                ResetKruxEffectAudio();
                kruxEffectAudio.clip = kruxAttacking;
                kruxEffectAudio.Play();
                break;
            case "Xygo":
                ResetXygoEffectAudio();
                xygoEffectAudio.clip = xygoAttacking;
                xygoEffectAudio.Play();
                break;
            default:
                break;
        }

        return;
    }

    public void PlayDeployed(string _name)
    {
        switch(_name)
        {
            // Play the audio for each character placement.
            case "Vessel":
                ResetVesselVoiceAudio();
                vesselVoiceAudio.clip = vesselDeployed;
                vesselVoiceAudio.Play();
                break;
            case "Odo":
                ResetOdoVoiceAudio();
                odoVoiceAudio.clip = odoDeployed;
                odoVoiceAudio.Play();
                break;
            case "Krux":
                ResetKruxVoiceAudio();
                kruxVoiceAudio.clip = kruxDeployed;
                kruxVoiceAudio.Play();
                break;
            case "Xygo":
                ResetXygoVoiceAudio();
                xygoVoiceAudio.clip = xygoDeployed;
                xygoVoiceAudio.Play();
                break;
            default:
                break;
        }

        return;
    }

    public void PlayLowHealth(string _name)
    {
        switch (_name)
        {
            // Play the audio for each character placement.
            case "Vessel":
                ResetVesselVoiceAudio();
                vesselVoiceAudio.clip = vesselLowHealth;
                vesselVoiceAudio.Play();
                break;
            case "Odo":
                ResetOdoVoiceAudio();
                odoVoiceAudio.clip = odoLowHealth;
                odoVoiceAudio.Play();
                break;
            case "Krux":
                ResetKruxVoiceAudio();
                kruxVoiceAudio.clip = kruxLowHealth;
                kruxVoiceAudio.Play();
                break;
            case "Xygo":
                ResetXygoVoiceAudio();
                xygoVoiceAudio.clip = xygoLowHealth;
                xygoVoiceAudio.Play();
                break;
            default:
                break;
        }

        return;
    }

    public void PlayHealing(string _name)
    {
        switch (_name)
        {
            // Play the audio for each character placement.
            case "Vessel":
                ResetVesselVoiceAudio();
                vesselVoiceAudio.clip = vesselHealing;
                vesselVoiceAudio.Play();
                break;
            case "Odo":
                ResetOdoVoiceAudio();
                odoVoiceAudio.clip = odoHealing;
                odoVoiceAudio.Play();
                break;
            case "Krux":
                ResetKruxVoiceAudio();
                kruxVoiceAudio.clip = kruxHealing;
                kruxVoiceAudio.Play();
                break;
            case "Xygo":
                ResetXygoVoiceAudio();
                xygoVoiceAudio.clip = xygoHealing;
                xygoVoiceAudio.Play();
                break;
            default:
                break;
        }

        return;
    }

    public void PlayFullHealth(string _name)
    {
        switch (_name)
        {
            // Play the audio for each character placement.
            case "Vessel":
                ResetVesselVoiceAudio();
                vesselVoiceAudio.clip = vesselFullyHealed;
                vesselVoiceAudio.Play();
                break;
            case "Odo":
                ResetOdoVoiceAudio();
                odoVoiceAudio.clip = odoFullyHealed;
                odoVoiceAudio.Play();
                break;
            case "Krux":
                ResetKruxVoiceAudio();
                kruxVoiceAudio.clip = kruxFullyHealed;
                kruxVoiceAudio.Play();
                break;
            case "Xygo":
                ResetXygoVoiceAudio();
                xygoVoiceAudio.clip = xygoFullyHealed;
                xygoVoiceAudio.Play();
                break;
            default:
                break;
        }

        return;
    }

    private void ResetVesselEffectAudio()
    {
        if (vesselEffectAudio.isPlaying)
        {
            vesselEffectAudio.Stop();
        }
        return;
    }

    private void ResetVesselVoiceAudio()
    {
        if (vesselVoiceAudio.isPlaying)
        {
            vesselVoiceAudio.Stop();
        }
        return;
    }

    private void ResetOdoEffectAudio()
    {
        if (odoEffectAudio.isPlaying)
        {
            odoEffectAudio.Stop();
        }
        return;
    }

    private void ResetOdoVoiceAudio()
    {
        if (odoVoiceAudio.isPlaying)
        {
            odoVoiceAudio.Stop();
        }
        return;
    }

    private void ResetKruxEffectAudio()
    {
        if (kruxEffectAudio.isPlaying)
        {
            kruxEffectAudio.Stop();
        }
        return;
    }

    private void ResetKruxVoiceAudio()
    {
        if (kruxVoiceAudio.isPlaying)
        {
            kruxVoiceAudio.Stop();
        }
        return;
    }

    private void ResetXygoEffectAudio()
    {
        if (xygoEffectAudio.isPlaying)
        {
            xygoEffectAudio.Stop();
        }
        return;
    }

    private void ResetXygoVoiceAudio()
    {
        if (xygoVoiceAudio.isPlaying)
        {
            xygoVoiceAudio.Stop();
        }
        return;
    }
}
