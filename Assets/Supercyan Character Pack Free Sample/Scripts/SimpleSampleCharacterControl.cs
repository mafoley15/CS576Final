using UnityEngine;

/*
This script was taken from Unity Store Asset Pack: 
https://assetstore.unity.com/packages/3d/characters/humanoids/character-pack-free-sample-79870

It was modified to simplify it, removing unnecessary features while also adding audio clips for when walking
*/
namespace Supercyan.FreeSample
{
    public class SimpleSampleCharacterControl : MonoBehaviour
    {

        private float curV = 0;
        private float curH = 0;

        [SerializeField] private float mSpeed = 2;
        [SerializeField] private float tSpeed = 200;

        [SerializeField] private Animator animator = null;
        [SerializeField] private Rigidbody rb = null;

        [SerializeField] public AudioSource audioSource;
        [SerializeField] public AudioClip footstep;
        [SerializeField] private float interval = 0.4f; // Adjust based on desired interval

        private float stepTimer = 0f;

        private void Awake()
        {
            if (!rb) { rb = GetComponent<Rigidbody>(); }
            if (!animator) { animator = GetComponent<Animator>(); }
        }

        private void TankUpdate(){

            float vert = Input.GetAxis("Vertical");
            float hor = Input.GetAxis("Horizontal");


            bool isMoving = Mathf.Abs(vert) > 0.1f;

            if (isMoving){
                stepTimer += Time.deltaTime;
                if (stepTimer >= interval){
                    PlayFootstepSound();
                    stepTimer = 0f;
                }
            } else{
                stepTimer = 0f; 
            } 

            if (vert < 0){
                vert *= 0.6f;
            }

            curV = Mathf.Lerp(curV, vert, Time.deltaTime * 10);
            curH = Mathf.Lerp(curH, hor, Time.deltaTime * 10);

            transform.position += transform.forward * curV * mSpeed * Time.deltaTime;
            transform.Rotate(0, curH * tSpeed * Time.deltaTime, 0);

            animator.SetFloat("MoveSpeed", curV);
        }

        private void FixedUpdate(){
            animator.SetBool("Grounded", true);
            TankUpdate();
        }

        private void PlayFootstepSound(){
            audioSource.volume = 0.15f;
            audioSource.PlayOneShot(footstep);
        }
    }
}