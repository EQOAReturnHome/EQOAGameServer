// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ReturnHome.Utilities
{
    public static class CharacterUtilities
    {
        //Dictionary mapping the client value to the string value expected in the DB
        //Eastern Human = 1, Western Human = 2, All non humans = 0
        public static readonly Dictionary<int, string> HumTypeDict = new Dictionary<int, string>
        {
            {0, "Other" },
            {1, "Freeport" },
            {2, "Qeynos" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Class
        public static readonly Dictionary<int, string> CharClassDict = new Dictionary<int, string>
        {
            {0, "WAR" },  {1, "RAN" }, {2, "PAL" }, {3, "SK" }, {4, "MNK" },{5, "BRD" }, {6, "RGE" },
            {7, "DRD" }, {8, "SHA" }, {9, "CL" }, {10, "MAG" }, {11, "NEC" },{12, "ENC" }, {13, "WIZ" }, {14, "ALC" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Race
        public static readonly Dictionary<int, string> CharRaceDict = new Dictionary<int, string>
        {
            {0, "HUM" }, {1, "ELF" }, {2, "DELF" }, {3, "GNO" }, {4, "DWF" }, {5, "TRL" }, {6, "BAR" }, {7, "HLF" },
            {8, "ERU" }, {9, "OGR" }
        };

        //Dictionary mapping the client value to the string value expected in the DB for Player Gender
        public static readonly Dictionary<int, string> CharSexDict = new Dictionary<int, string>
        {
            {0, "Male" }, {1, "Female"}
        };
    }
}
