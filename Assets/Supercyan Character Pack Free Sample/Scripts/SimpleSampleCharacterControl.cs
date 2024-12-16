using UnityEngine;

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

        private void Awake()
        {
            if (!rb) { rb = GetComponent<Rigidbody>(); }
            if (!animator) { animator = GetComponent<Animator>(); }
        }

        private void TankUpdate(){
            float vert = Input.GetAxis("Vertical");
            float hor = Input.GetAxis("Horizontal");

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
    }
}