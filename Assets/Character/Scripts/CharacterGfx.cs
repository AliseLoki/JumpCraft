using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterGfx : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] private JumpController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] private JumpHandler controller;

    private void OnEnable()
    {
        controller.OnPrepareJump += PrepareJump;
        controller.OnJump += Jump;
        controller.OnLand += Land;
        controller.OnDrop += Miss;
    }

    private void OnDisable()
    {
        controller.OnPrepareJump -= PrepareJump;
        controller.OnJump -= Jump;
        controller.OnLand -= Land;
        controller.OnDrop -= Miss;
    }

    private void Update()
    {
        if(!controller) return;
        
        animator.SetFloat("Power", controller.GetCurrentPower/controller.GetMaxJumpPower);
    }

    private void PrepareJump()
    {
        animator.SetTrigger("PrepareJump");
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("OnAir", true);
        dustParticles.Play();
    }

    private void Land()
    {
        animator.SetBool("OnAir", false);
        dustParticles.Play();
    }

    private void Miss()
    {
        animator.SetTrigger("Miss");
        //Invoke("Respawn", 1f);
    }

    /*private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
