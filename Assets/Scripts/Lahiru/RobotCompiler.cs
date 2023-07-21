using UnityEngine;
using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;

#region Aliases
using MoveCommand = RobotProgram.MoveCommand;
using RotateCommand = RobotProgram.RotateCommand;
using JumpCommand = RobotProgram.JumpCommand;
using MoveDirection = RobotProgram.MoveCommand.Direction;
using PickCommand = RobotProgram.PickCommand;
using DropCommand = RobotProgram.DropCommand;
#endregion


/**
 * Just an implementation of the plain old builder design pattern. 
 */
public class RobotCompiler
{

    private List<RobotProgram.Command> _commands;
    private List<RobotProgram.Command> Commands
    {
        get { return _commands; }
    }

    public RobotCompiler()
    {
        

        _commands = new List<RobotProgram.Command>();

        
    }

    public static RobotProgram Compile(string source)
    {
        AntlrInputStream antlrStream = new AntlrInputStream(source);
        RobotLexer lexer = new RobotLexer(antlrStream);
        CommonTokenStream tokenStream = new CommonTokenStream(lexer);
        RobotParser parser = new RobotParser(tokenStream);

        parser.code(); // <-- compile actually happens here (see Assets/Grammars/Robot/Robot.g4)

        RobotCompiler compiler = parser.Compiler;
        RobotProgram program = new RobotProgram(compiler.Commands);

        return program;
    }

    public bool ifFlag = false;
    public bool onlyIfFlag = false;
    public bool onlyWhileFlag = false;
    float horizantal = 0;
    float vertical = 0;

    public RobotCompiler AddMoveCommand(string direction)
    {
        if (ifFlag == false)
        {

            //Debug.Log("Text: " + direction);
            MoveDirection moveDirection;
            switch (direction)
            {
                case "moveForward()":
                    moveDirection = MoveDirection.FORWARD;
                    vertical = vertical + 1.0f ; 
                    break;
                case "moveBackward()":
                    moveDirection = MoveDirection.BACKWARD;
                    vertical = vertical - 1.0f;
                    break;
                case "moveLeft()":
                    moveDirection = MoveDirection.LEFT;
                    horizantal = horizantal - 1.0f;
                    break;
                case "moveRight()":
                    moveDirection = MoveDirection.RIGHT;
                    horizantal = horizantal + 1.0f;
                    break;
                default:
                    Debug.LogError("Invalid move command direction: " + direction);
                    return this;
            }

            _commands.Add(new MoveCommand(moveDirection));
            return this;
        }
        else
        {
            return this;
        }
    }

    private bool isPickup = false;

    public RobotCompiler AddPickCommand(string direction)
    {
        //private float angle;
        isPickup = true;
        Vector3 pickDirection;
        switch (direction)
        {
            case "pickForward()":
                pickDirection = Vector3.forward;
                break;
            case "pickBackward()":
                pickDirection = -Vector3.forward;
                break;
            case "pickLeft()":
                pickDirection = -Vector3.right;
                break;
            case "pickRight()":
                pickDirection = Vector3.right;
                break;
            default:
                Debug.LogError("Invalid move command direction: " + direction);
                return this;
        }

        _commands.Add(new PickCommand(pickDirection));
        return this;
    }

    public RobotCompiler AddDropCommand()
    {
       if(isPickup == true)
        {
            _commands.Add(new DropCommand());
            return this;
        }
        return this;
    }


        public RobotCompiler AddRotateCommand(string direction)
    {
        //private float angle;
        float anglee;
        switch (direction)
        {
            case "rotateLeft()":
                anglee = 90;
                break;
            case "rotateRight()":
                anglee = -90;
                break;
            default:
                Debug.LogError("Invalid move command direction: " + direction);
                return this;
        }

        _commands.Add(new RotateCommand(anglee));
        return this;
    }

    public RobotCompiler AddCheckIfCommand(string condition)
    {

        Debug.Log("Text: " + condition);

        ifFlag = true;
        onlyIfFlag = true;

        return this;
    }

    public RobotCompiler AddIfCommand(string scanDirection, string condition, string barrier)
    {
        Debug.Log("Scan Direction : " + scanDirection);
        if ((ifFlag == true) && (onlyIfFlag = true))
        {
            switch (scanDirection)
            {
                case "scanForward()":
                    if (barrier == "obstacle" && condition == "==")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
                        //bool result = program.CheckCondition(this.gameObject);

                        if (result == false)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }

                    }
                    else if (barrier == "obstacle" && condition == "!=")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
                        //bool result = program.CheckCondition(this.gameObject);
                        if (result == true)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    };
                    break;

                case "scanBackward()":
                    if (barrier == "obstacle" && condition == "==")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);
                        if (result == false)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    }
                    else if (barrier == "obstacle" && condition == "!=")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);
                        if (result == true)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    };
                    break;
                case "scanRight()":
                    if (barrier == "obstacle" && condition == "==")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);
                        if (result == false)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    }
                    else if (barrier == "obstacle" && condition == "!=")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);
                        if (result == true)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    };
                    break;
                case "scanLeft()":
                    Debug.Log("yo yo : " + scanDirection);
                    if (barrier == "obstacle" && condition == "==")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                        if (result == false)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    }
                    else if (barrier == "obstacle" && condition == "!=")
                    {
                        bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                        if (result == true)
                        {
                            ifFlag = false;
                        }
                        else
                        {
                            ifFlag = true;
                        }
                    };
                    break;
            }

        }

        return this;
    }

    public RobotCompiler AddMoveIntCommand(string direction, string distance)
    {
        Debug.Log("Text: " + direction);
        int number = int.Parse(distance);
        for (int i = 0; i < number; i++)
        {
            if (ifFlag == false)
            {

                Debug.Log("Text: " + direction);
                MoveDirection moveDirection;
                switch (direction)
                {
                    case "moveForward":
                        moveDirection = MoveDirection.FORWARD;
                        vertical = vertical + 1.0f;
                        break;
                    case "moveBackward":
                        moveDirection = MoveDirection.BACKWARD;
                        vertical = vertical - 1.0f;
                        break;
                    case "moveLeft":
                        moveDirection = MoveDirection.LEFT;
                        horizantal = horizantal - 1.0f;
                        break;
                    case "moveRight":
                        moveDirection = MoveDirection.RIGHT;
                        horizantal = horizantal + 1.0f;
                        break;
                    default:
                        Debug.LogError("Invalid move command direction: " + direction);
                        return this;
                }

                _commands.Add(new MoveCommand(moveDirection));
            }

        }

        return this;
    }
    public RobotCompiler AddJumpCommand(string direction)
    {


        Debug.Log("Text: " + direction);



        if (ifFlag == false)
        {

            Debug.Log("Text: " + direction);
            Vector3 jumpDirection;
            switch (direction)
            {
                case "jumpForward()":
                    jumpDirection = Vector3.forward;
                    vertical = vertical + 2.0f;
                    break;
                case "jumpBackward()":
                    jumpDirection = -Vector3.forward;
                    vertical = vertical - 2.0f;
                    break;
                case "jumpLeft()":
                    jumpDirection = -Vector3.right;
                    horizantal = horizantal - 2.0f;
                    break;
                case "jumpRight()":
                    jumpDirection = Vector3.right;
                    horizantal = horizantal + 2.0f;
                    break;
                default:
                    Debug.LogError("Invalid move command direction: " + direction);
                    return this;
            }

            _commands.Add(new JumpCommand(jumpDirection));
            return this;
        }
        else
        {
            return this;
        }
       
    }

    public RobotCompiler AddCheckForCommand()
    {
        ifFlag = true;

        Debug.Log("for ");
        return this;
    }

    public RobotCompiler AddForCommand(string int1, string condition, string int2, string direction, string code)
    {
        Debug.Log("Text: " + ifFlag);



        if (ifFlag == true)
        {
            if ((code == "moveForward()") || (code == "moveBackward()") || (code == "moveRight()") || code == "moveLeft()")
            {
                MoveDirection moveDirection;
            switch (code)
            {
                case "moveForward()":
                    moveDirection = MoveDirection.FORWARD;
                        vertical = vertical + 1.0f;
                        break;
                case "moveBackward()":
                    moveDirection = MoveDirection.BACKWARD;
                        vertical = vertical - 1.0f;
                        break;
                case "moveLeft()":
                    moveDirection = MoveDirection.LEFT;
                        horizantal = horizantal - 1.0f;
                        break;
                case "moveRight()":
                    moveDirection = MoveDirection.RIGHT;
                        horizantal = horizantal + 1.0f;
                        break;
                default:
                    Debug.LogError("Invalid move command direction: " + code);
                    ifFlag = false;
                    return this;
            }
            int number1 = int.Parse(int1);
            int number2 = int.Parse(int2);

                if (direction == "++")
                {
                    switch (condition)
                    {
                        case "<":
                            for (int i = number1; i < number2; i++)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case ">":
                            for (int i = number1; i > number2; i++)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case "<=":
                            for (int i = number1; i <= number2; i++)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case ">=":
                            for (int i = number1; i >= number2; i++)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                    }
                }
                else if (direction == "--")
                {
                    switch (condition)
                    {
                        case "<":
                            for (int i = number1; i < number2; i--)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case ">":
                            for (int i = number1; i > number2; i--)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case "<=":
                            for (int i = number1; i <= number2; i--)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                        case ">=":
                            for (int i = number1; i >= number2; i--)
                            {
                                _commands.Add(new MoveCommand(moveDirection));
                            };
                            break;
                    }
                }
            }
            else if ((code == "jumpForward()") || (code == "jumpBackward()") || (code == "jumpRight()") || code == "jumpLeft()")
            {

                Vector3 jumpDirection;
                switch (code)
                {
                    case "jumpForward()":
                        jumpDirection = Vector3.forward;
                        vertical = vertical + 2.0f;
                        break;
                    case "jumpBackward()":
                        jumpDirection = -Vector3.forward;
                        vertical = vertical - 2.0f;
                        break;
                    case "jumpLeft()":
                        jumpDirection = -Vector3.right;
                        horizantal = horizantal - 2.0f;
                        break;
                    case "jumpRight()":
                        jumpDirection = Vector3.right;
                        horizantal = horizantal + 2.0f;
                        break;
                    default:
                        Debug.LogError("Invalid move command direction: " + direction);
                        return this;
                }

                int number1 = int.Parse(int1);
                int number2 = int.Parse(int2);

                if (direction == "++")
                {
                    switch (condition)
                    {
                        case "<":
                            for (int i = number1; i < number2; i++)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case ">":
                            for (int i = number1; i > number2; i++)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case "<=":
                            for (int i = number1; i <= number2; i++)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case ">=":
                            for (int i = number1; i >= number2; i++)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                    }
                }
                else if (direction == "--")
                {
                    switch (condition)
                    {
                        case "<":
                            for (int i = number1; i < number2; i--)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case ">":
                            for (int i = number1; i > number2; i--)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case "<=":
                            for (int i = number1; i <= number2; i--)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                        case ">=":
                            for (int i = number1; i >= number2; i--)
                            {
                                _commands.Add(new JumpCommand(jumpDirection));
                            };
                            break;
                    }
                }

                _commands.Add(new JumpCommand(jumpDirection));

            }


        }
        ifFlag = false;
        return this;

    }

    public RobotCompiler AddCheckWhileCommand()
    {
        ifFlag = true;
        onlyWhileFlag = true;

        Debug.Log("while ");
        return this;
    }
    
    public RobotCompiler AddWhileCommand(string scanDirection ,string condition, string barrier, string code )
    {
        Debug.Log("Text: " +code + ifFlag + onlyWhileFlag);
        bool result2 = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
        if (code == "moveForward()")
        {
            Debug.Log("Text: " + code);
        }

        if ((ifFlag == true) && (onlyWhileFlag == true))
        {
            if ((code == "moveForward()") || (code == "moveBackward()") || (code == "moveRight()") || code == "moveLeft()")
            {
                MoveDirection moveDirection;
                switch (code)
                {
                    case "moveForward()":
                        moveDirection = MoveDirection.FORWARD;
                        
                        break;
                    case "moveBackward()":
                        moveDirection = MoveDirection.BACKWARD;
                        
                        break;
                    case "moveLeft()":
                        moveDirection = MoveDirection.LEFT;
                        
                        break;
                    case "moveRight()":
                        moveDirection = MoveDirection.RIGHT;
                        
                        break;
                    default:
                        Debug.LogError("Invalid move command direction: " + code);
                        ifFlag = false;
                        return this;
                }


                switch (scanDirection)
                {
                    case "Forward()":
                        if (barrier == "obstacle" && condition == "==")
                        {

                            bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);

                            while (result == false)
                            {
                                if(moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);



                            }

                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
                            Debug.LogError(result + "   " + moveDirection);

                            while (result == true)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
  

                            }


                        };
                        break;

                    case "Backward()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            while (result == false)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            while (result == true)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            }

                        };
                        break;
                    case "Right()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            while (result == false)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            while (result == true)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            }

                        };
                        break;
                    case "Left()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);


                            while (result == false)
                            {
                                if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);


                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                            

                            while (result == true)
                            {
                            if (moveDirection == MoveDirection.FORWARD)
                                {
                                    vertical = vertical + 1.0f;
                                }
                                else if (moveDirection == MoveDirection.BACKWARD)
                                {
                                    vertical = vertical - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.LEFT)
                                {
                                    horizantal = horizantal - 1.0f;
                                }
                                else if (moveDirection == MoveDirection.RIGHT)
                                {
                                    horizantal = horizantal + 1.0f;
                                }
                                _commands.Add(new MoveCommand(moveDirection));
                                result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                           




                            }

                        };
                        break;

                }
            }

            else if ((code == "jumpForward()") || (code == "jumpBackward()") || (code == "jumpRight()") || code == "jumpLeft()")
            {

                Vector3 jumpDirection;
                switch (code)
                {
                    case "jumpForward()":
                        jumpDirection = Vector3.forward;

                        break;
                    case "jumpBackward()":
                        jumpDirection = -Vector3.forward;

                        break;
                    case "jumpLeft()":
                        jumpDirection = -Vector3.right;

                        break;
                    case "jumpRight()":
                        jumpDirection = Vector3.right;

                        break;
                    default:
                        Debug.LogError("Invalid move command direction: ");
                        return this;
                }

                switch (scanDirection)
                {
                    case "Forward()":
                        if (barrier == "obstacle" && condition == "==")
                        {

                            bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);

                            while (result == false)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }

                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);

                            }



                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);

                            while (result == false)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastForward(vertical, horizantal);
                            }


                        };
                        break;

                    case "Backward()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            while (result == false)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);
                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);


                            while (result == true)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastBackward(vertical, horizantal);
                            }

                        };
                        break;
                    case "Right()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            while (result == false)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);
                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);


                            while (result == true)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastRight(vertical, horizantal);
                            }

                        };
                        break;
                    case "Left()":
                        if (barrier == "obstacle" && condition == "==")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);


                            while (result == false)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                            }


                        }
                        else if (barrier == "obstacle" && condition == "!=")
                        {
                            bool result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);


                            while (result == true)
                            {
                                if (jumpDirection == Vector3.forward)
                                {
                                    vertical = vertical + 2.0f;
                                }
                                else if (jumpDirection == -Vector3.forward)
                                {
                                    vertical = vertical - 2.0f;
                                }
                                else if (jumpDirection == -Vector3.right)
                                {
                                    horizantal = horizantal - 2.0f;
                                }
                                else if (jumpDirection == Vector3.right)
                                {
                                    horizantal = horizantal + 2.0f;
                                }
                                _commands.Add(new JumpCommand(jumpDirection));
                                result = CheckTrigger.instance.PerformRaycastLeft(vertical, horizantal);
                            }

                        };
                        break;

                }

            }

            

            ifFlag = false;
            return this;
        }
        else
        {
            return this;
        }
        
    }

    public RobotCompiler AddJumpIntCommand(string direction, string distance)
    {
        int number = int.Parse(distance);
        for (int i = 0; i < number; i++)
        {
            if (ifFlag == false)
            {

                Debug.Log("Text: " + direction);
                Vector3 jumpDirection;
                switch (direction)
                {
                    case "jumpForward":
                        jumpDirection = Vector3.forward;
                        vertical = vertical + 2.0f;
                        break;
                    case "jumpBackward":
                        jumpDirection = -Vector3.forward;
                        vertical = vertical - 2.0f;
                        break;
                    case "jumpLeft":
                        jumpDirection = -Vector3.right;
                        horizantal = horizantal - 2.0f;
                        break;
                    case "jumpRight":
                        jumpDirection = Vector3.right;
                        horizantal = horizantal + 2.0f;
                        break;
                    default:
                        Debug.LogError("Invalid move command direction: " + direction);
                        return this;
                }

                _commands.Add(new JumpCommand(jumpDirection));
            }


        }
        return this;
    }
}
