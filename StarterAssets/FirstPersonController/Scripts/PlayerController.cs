using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public CharacterController characterController;
    public AudioSource footstepAudioSource;

    void Start()
    {
        if (characterController == null)
        {
            // Eðer karakter kontrol sistemi atanmamýþsa, bu scriptin baðlý olduðu GameObject üzerindeki CharacterController bileþenini al.
            characterController = GetComponent<CharacterController>();
        }

        if (footstepAudioSource == null)
        {
            // Eðer AudioSource atanmamýþsa, bu scriptin baðlý olduðu GameObject üzerindeki AudioSource bileþenini al.
            footstepAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        PlayFootstepSounds();
    }

    void PlayFootstepSounds()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0)
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else
        {
            footstepAudioSource.Stop();
        }
    }
}
