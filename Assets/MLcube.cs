using Unity.MLAgents;
using UnityEngine;

public class MLcube : Agent
{
    public float Force = 15f;
    private Rigidbody rb = null;
    public Transform orig = null;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 1)
        {
            Trust();
        }
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {

            actionsOut[0] = 1;
        }
    }
    
    public override void OnEpisodeBegin()
    {
        ResetCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            AddReward(-1.0f);
            DestroyObject(collision.gameObject);
            EndEpisode();
        }
        if (collision.gameObject.tag == "topWall")
        {
            AddReward(-1.0f);
            EndEpisode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wallReward")==true)
        {
            AddReward(0.1f);
        }
    }

    private void Trust()
    {
        rb.AddForce(Vector3.up * Force, ForceMode.Acceleration);
    }
    private void ResetCube()
    {

        this.transform.position = new Vector3(orig.position.x, orig.position.y, orig.position.z);
    }

}
