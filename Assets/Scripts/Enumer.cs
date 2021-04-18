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

public class Enumer : MonoBehaviour
{
    public float Speed = 1.0f;
    public List<BallInstruction> Instructions = new List<BallInstruction>();
    public int CurrentInstruction = 0;
    public float InstructionLenght = 0.0f;

    public Vector3 CurrentPosition;
    public Vector3 StartPosition;

    public void Start()
    {
        StartPosition = transform.position;
    }

    public void Update()
    {
        if (CurrentInstruction < Instructions.Count)
        {
            float RealSpeed = Speed * Time.deltaTime;
            float InsctructionLenght = Vector3.Distance(StartPosition, CurrentPosition);

            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;
                    CurrentPosition = transform.position;
                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;
                    CurrentPosition = transform.position;
                    break;

                case BallInstruction.MoveLeft:
                    transform.position += Vector3.left * RealSpeed;
                    CurrentPosition = transform.position;
                    break;

                case BallInstruction.MoveRight:
                    transform.position += Vector3.right * RealSpeed;
                    CurrentPosition = transform.position;
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }

            if(InsctructionLenght > 1.0f)
            {
                InsctructionLenght = 0.0f;
                StartPosition = CurrentPosition;
                ++CurrentInstruction;
            }
        }
    }
}
