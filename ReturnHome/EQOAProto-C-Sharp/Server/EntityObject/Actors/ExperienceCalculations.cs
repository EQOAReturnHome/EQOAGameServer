// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReturnHome.Server.EntityObject.Actors
{
    public static class ExperienceCalculations
    {
        //TODO Add party calculations later
        public static int CalculateMobXP(int mobLevel, int playerLevel)
        {
            int XP;
            if (mobLevel <= 15)
            {
                Console.WriteLine($"Mob level is {mobLevel}");
                Console.WriteLine($"Player level is {playerLevel}");
                XP = 0;
            }
            else
            {
                //int groupModifier = 1 + 0.1*(())
                XP = (int)(Math.Pow((mobLevel + 1), 2) * 40);
                XP *= (int)Math.Round(1 + (.05 * (mobLevel - playerLevel)));
            }


            int difference = (playerLevel - mobLevel);

            switch (difference)
            {
                //Red con target
                case <= -3:
                //Yellow con'ing target
                case -2:
                case -1:
                //White con target
                case 0:
                //Dark Blue con target
                case 1:
                case 2:
                    //int groupModifier = 1 + 0.1*(())
                    XP = (int)(Math.Pow((mobLevel + 1), 2) * 40);
                    XP *= (int)Math.Round(1 + (.05 * (mobLevel - playerLevel)));
                    return XP;

                //Decide Light blue and green con targets here
                default:

                    //return (byte)(Level <= 15 ? (Level - _ourTarget.Level) > 3 ? 5 : 4 : (Level - _ourTarget.Level) > (Level / 4) ? 5 : 4);
                    if (playerLevel <= 15)
                    {
                        //Green con target
                        if ((playerLevel - mobLevel) > 3)
                            XP = 0;

                        //Light blue con target
                        else
                            return 4;
                    }

                    else
                    {
                        //Green con target
                        if ((playerLevel - mobLevel) > (playerLevel / 4))
                            XP = 0;

                        //Light blue con target
                        else
                            return 4;
                    }
                    return XP;
            }
        }
    }
}
