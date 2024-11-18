using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using NUnit;
using Unity.VisualScripting;
public class MoveToBallAgent : Agent
{
    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin() {
        transform.position = new Vector3(0f, 3.0f, 0f);
        Debug.Log("Began episode");
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        Debug.Log("Action Recieved");

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //int ball_action = actions.DiscreteActions[0];
        float moveSpeed = 5.0f;

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered");

        if (other.gameObject.CompareTag("Ball")) {

            SetReward(1f);
            EndEpisode();
        }
        if (other.gameObject.CompareTag("Wall")) {
            SetReward(-1f);
            EndEpisode();
        } 
    }

    private void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Ball")) {
            Debug.Log("Ball hit");

            SetReward(1f);
            EndEpisode();
        }
        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("Wall hit");

            SetReward(-1f);
            EndEpisode();
        } 
    }


}