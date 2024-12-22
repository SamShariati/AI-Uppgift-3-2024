using UnityEngine;

public class MoveAgent : MonoBehaviour
{

    AgentManager agent;

    private void Awake()
    {
        agent = GetComponent<AgentManager>(); 
    }
    public void Move(float FB, float LR)
    {
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, 0, 2);

        if (!agent.isDead)
        {
            //Rotera höger och vänster
            transform.Rotate(0, LR * agent.rotationSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            //controller.SimpleMove(forward * speed * FB * -1);
        }
    }
}
