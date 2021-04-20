using System.Collections.Generic;
using UnityEngine;

public enum BallInstruction
{
    Idle = 0,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight
}

public class BallComponents : MonoBehaviour
{
    public float speed = 1.0f;
    public List<BallInstruction> instructions = new List<BallInstruction>();
    public int currentInstructionIndex = 0;
    public float distance = 1.0f;

    public Vector3 currentPosition;
    public Vector3 startPosition;

    public void Start()
    {
        startPosition = transform.position;
    }

    public void Update()
    {
        if (currentInstructionIndex >= instructions.Count)
            return;

        float realSpeed = speed * Time.deltaTime;
        float insctructionLenght = Vector3.Distance(startPosition, currentPosition);

        switch (instructions[currentInstructionIndex])
        {
            case BallInstruction.MoveUp:
                transform.position += Vector3.up * realSpeed;
                currentPosition = transform.position;
                break;

            case BallInstruction.MoveDown:
                transform.position += Vector3.down * realSpeed;
                currentPosition = transform.position;
                break;

            case BallInstruction.MoveLeft:
                transform.position += Vector3.left * realSpeed;
                currentPosition = transform.position;
                break;

            case BallInstruction.MoveRight:
                transform.position += Vector3.right * realSpeed;
                currentPosition = transform.position;
                break;

            default:
                Debug.Log("Idle");
                break;
        }

        if (insctructionLenght > distance)
        {
            startPosition = currentPosition;
            ++currentInstructionIndex;
        }
    }
}
