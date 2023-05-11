﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnHome.Server.EntityObject.Actors
{
    public static class Respawn
    {


        public static void ResetActor(Actor actor)
        {
            Console.WriteLine($"Resetting actor - {actor.CharName}");
            actor._killTime = 0;
            actor.despawn = false;
            actor.CurrentHP = actor.GetMaxHP();
            actor.Dead = false;
            actor.respawn = false;
            actor.HPFlag = true;
            actor.Animation = (byte)AnimationState.Default;
            actor.aggroTable.Clear();
            Console.WriteLine(actor.killtime);
            Console.WriteLine(actor.CurrentHP);
            Console.WriteLine(actor.Animation);


        }

    }
}
