﻿-- coachman boblon

local coaches = require('Scripts/ports')

local playerCoaches = {
   freeport_coach = "Get me a horse to Freeport.",
   klick_coach = "Get me a horse to Klick'Anon'.",
   rivervale_coach = "Get me a horse to Rivervale."
}

local ch = tostring(choice)
SetPlayerFlags(mySession, "admin", true)
function event_say()
   dialogueOptions = {}
   if(GetPlayerFlags(mySession, "bobble_by_water_coach")) then
      if (ch:find("Freeport")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.freeport.world),
         coaches.freeport.x,
         coaches.freeport.y,
         coaches.freeport.z,
         coaches.freeport.facing
         )
      elseif (ch:find("Klick")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.klick_anon.world),
         coaches.klick_anon.x,
         coaches.klick_anon.y,
         coaches.klick_anon.z,
         coaches.klick_anon.facing
         )
      elseif (ch:find("Rivervale")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.rivervale.world),
         coaches.rivervale.x,
         coaches.rivervale.y,
         coaches.rivervale.z,
         coaches.rivervale.facing
         )
      else
         npcDialogue = "Where would you like to go?"
         for coach, diag in pairs(playerCoaches) do
            if (GetPlayerFlags(mySession, coach) or GetPlayerFlags(mySession, "admin")) then
               table.insert(dialogueOptions, diag)
            end

         end
                     SendDialogue(mySession, npcDialogue, dialogueOptions)

      end
   else

      if (ch:find("Yes")) then
         npcDialogue = "Excellent, you can now use this coach any time."
         SetPlayerFlags(mySession, "bobble_by_water_coach", true)
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      elseif (ch:find("No")) then
         npcDialogue = "If you aren't interested then why are you wasting my time."
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      else
         npcDialogue = "Would you like to sign the coachman's ledger?"
         dialogueOptions = {"Yes", "No"}
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      end
   end
end


