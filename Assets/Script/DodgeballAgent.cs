using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using NUnit;
using Unity.VisualScripting;
public class DodgeballAgent : Agent
{
    public float ballCollisionReward = 1f;
    public float wallCollisionReward = -1f;
    public float playerSpeed = 3f;
    [SerializeField] private Transform targetTransform;
    [SerializeField] List<GameObject> balls;
    [SerializeField] float distanceToPickUp = 2f;


    public override void OnEpisodeBegin() {
        int id = 0;
        if (transform.CompareTag("Player")) {
            id = 1;
        } else {
            id = -1;
        }
        transform.position = new Vector3(0f, 0.5f, 3.0f * id);

        foreach (GameObject ball in balls) {
            BallScript script = ball.GetComponent<BallScript>();
            script.OnEpisodeBegin();
        }

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
        int ball_action = actions.DiscreteActions[0];

        if (ball_action == 1) {
            pickupBall();
        } else if (ball_action == 2) {
            throwBall();
        } else {

        }

        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * playerSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        /*
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;


        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        /*
        /*
        if (Input.GetKey(KeyCode.E)) {
            discreteActions[0] = 1;
        }
        else if (Input.GetKey(KeyCode.R)) {
            discreteActions[0] = 2;
        }
        */
    }


    private void pickupBall() {
        foreach (GameObject ball in balls) {
            if (Vector3.Distance(ball.transform.position, transform.position) < distanceToPickUp) {
                BallScript script = ball.GetComponent<BallScript>();
                script.pickUpBall(transform, tag);
                break;
            }
        }
    }

    private void throwBall() {
        foreach (GameObject ball in balls) {
            if (Vector3.Distance(ball.transform.position, transform.position) < distanceToPickUp) {
                BallScript script = ball.GetComponent<BallScript>();
                script.throwBall(transform, tag);
                SetReward(ballCollisionReward);
                break;
            }
        }
    }



    private void OnTriggerEnter(Collider other) {
        /*
        Debug.Log("Triggered");

        if (other.gameObject.CompareTag("Ball")) {

            SetReward(ballCollisionReward);
            EndEpisode();
        }
        if (other.gameObject.CompareTag("Wall")) {
            SetReward(wallCollisionReward);
            EndEpisode();
        } 
        */
    }

    private void OnCollisionEnter(Collision other) {
        /*
        if (other.gameObject.CompareTag("Ball")) {
            Debug.Log("Ball hit");

            SetReward(ballCollisionReward);
            EndEpisode();
        }
        */
        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("Wall hit");

            SetReward(wallCollisionReward);
            EndEpisode();
        } 
        
    }


}