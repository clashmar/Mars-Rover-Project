﻿using MarsRover.Enums;
using MarsRover.Logic_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Input_Layer
{
    public static class InputControl
    {
        //Parse first input line as plateau size and if successful Set PlateauSize

        //Land first rover in a valid location

        //Execute the instructions

        //Repeat for all subsequent Rovers

        //Get final positions

        public static void ExecuteInput(List<String> input) 
        {
            ParsedPlateauSize parsedPlateauSize = new(input[0]);
            if(!parsedPlateauSize.IsValid) { Console.WriteLine("Not a valid plateau size."); return; }
            Plateau.plateauSize = parsedPlateauSize.PlateauSize;

            for(int i = 1; i < input.Count; i++)
            {
                if (i % 2 == 1)
                {
                    ParsedPosition parsedPosition = new(input[i]);
                    if (!parsedPosition.IsValid) { Console.WriteLine("This position is not valid."); return; }
                    if (!MissionControl.IsSafeToLand(parsedPosition.Position)) { Console.WriteLine("It's not safe to land here."); return; };
                    Rover newRover = new(parsedPosition.Position);

                    ParsedInstructions parsedInstructions = new(input[i + 1]);
                    if(!parsedInstructions.IsValid) { Console.WriteLine("These instructions are invalid."); return; }
                    foreach(Instruction instruction in parsedInstructions.Instructions)
                    {
                        newRover.ExecuteInstruction(instruction);
                    }
                    Console.WriteLine($"{newRover.Position.X} {newRover.Position.Y} {Enum.GetName(newRover.Position.Facing)}\n");
                }
            }
        }
    }
}