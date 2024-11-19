using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    Blue = 0,
    Orange = 1,
    Default = 2
}

public enum Event {
    HitBluePlayer = 0,
    HitOrangePlayer = 1,
}
public class DodgeballEnvController : MonoBehaviour
{
    int ballSpawnLocation;

    //DodgeballSettings dodgeballSettings;

    public DodgeballAgent blueAgent;
    public DodgeballAgent orangeAgent;

    public List<DodgeballAgent> AgentsList = new List<DodgeballAgent>();

    Rigidbody blueAgentRb;
    Rigidbody orangeAgentRb;

    public GameObject ball;

    Rigidbody ballRb;

    Team lastHitter;

    private int resetTimer;
    public int MaxEnvironmentSteps;
    public DodgeballEnvController envController;
    public GameObject blueAgentObject;
    public GameObject orangeAgentObject;
    Collider blueAgentCollider;
    Collider orangeAgentCollider;
    void Start() {
        // Use to control agents and ball starting position
        blueAgentRb = blueAgent.GetComponent<Rigidbody>();
        orangeAgentRb = orangeAgent.GetComponent<Rigidbody>();
        ballRb = ball.GetComponent<Rigidbody>();

        // Get Collider for each Player
        blueAgentCollider = blueAgentObject.GetComponent<Collider>();
        orangeAgentCollider = orangeAgentObject.GetComponent<Collider>();

        envController = GetComponentInParent<DodgeballEnvController>();
        // Starting ball Spawn on 0 Z-Axis
        ballSpawnLocation = Random.Range(-4,4);

        //dodgeballSettings = FindObjectOfType<DodgeballSettings>();

        ResetScene();
    }

    public void OnCollisionEnter(Collision other) {
        //if (other.gameObject.CompareTag(""))
    }
    // Tracks which agent was last holding the ball
    public void UpdateLastHitter(Team team) {
        lastHitter = team;
    }

    public void ResolveEvent(Event triggerEvent) {
        
    }

    void FixedUpdate() {

    }

    void ResetScene() {

    }

    void ResetBall() {

    }


}

