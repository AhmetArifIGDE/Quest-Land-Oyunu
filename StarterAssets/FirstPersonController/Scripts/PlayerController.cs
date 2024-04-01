using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public CharacterController characterController;
    public AudioSource footstepAudioSource;

    void Start()
    {
        if (characterController == null)
        {
            // E�er karakter kontrol sistemi atanmam��sa, bu scriptin ba�l� oldu�u GameObject �zerindeki CharacterController bile�enini al.
            characterController = GetComponent<CharacterController>();
        }

        if (footstepAudioSource == null)
        {
            // E�er AudioSource atanmam��sa, bu scriptin ba�l� oldu�u GameObject �zerindeki AudioSource bile�enini al.
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
