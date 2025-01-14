using UnityEngine;

public class MoveAgent : MonoBehaviour
{

    AgentManager agent;
    CharacterController cc;

    public float speed = 10.0F;
    public float rotateSpeed = 1.0F;

    public float agent_FB;
    public float agent_LR;

    private void Awake()
    {
        agent = GetComponent<AgentManager>(); 
        cc = GetComponent<CharacterController>();

    }
    public void Move(float FB, float LR)
    {
        //clamp the values of LR and FB
        agent_LR = Mathf.Clamp(LR, -1, 1);
        agent_FB = Mathf.Clamp(FB, 0, 2.5f);

        //move the agent
        if (!agent.isDead)
        {
            // Rotate around y - axis
            transform.Rotate(0, LR * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed * agent_FB * -1);
        }
    }
}
