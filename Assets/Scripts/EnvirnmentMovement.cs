using UnityEngine;

public class EnvirnmentMovement : MonoBehaviour
{    
    [SerializeField]private ParticleSystem leavesFX = default;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            anim.SetTrigger("move");
            leavesFX.Play();
        }
    }
}
