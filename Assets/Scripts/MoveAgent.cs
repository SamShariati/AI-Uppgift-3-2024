using UnityEngine;

public class MoveAgent : MonoBehaviour
{

    AgentManager agent;
    CharacterController cc;
    ObjectTracker objectTracker;

    public float speed = 10.0F;
    public float rotateSpeed = 10.0F;

    private void Awake()
    {
        agent = GetComponent<AgentManager>(); 
        cc = GetComponent<CharacterController>();

    }
    public void Move(float FB, float LR)
    {
        //clamp the values of LR and FB
        LR = Mathf.Clamp(LR, -1, 1);
        FB = Mathf.Clamp(FB, 0, 1);

        //move the agent
        if (!agent.isDead)
        {
            // Rotate around y - axis
            transform.Rotate(0, LR * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed * FB * -1);
        }
    }
}
