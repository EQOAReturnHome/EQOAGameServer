-- coachman boblon

local coaches = require('Scripts/ports')

local playerCoaches = {
   freeport_coach = "Get me a horse to Freeport.",
   klick_coach = "Get me a horse to Klick'Anon.",
   rivervale_coach = "Get me a horse to Rivervale."
}

 
function  event_say(choice)
   dialogueOptions = {}
   if(GetPlayerFlags(mySession, "bobble_by_water_coach") == "true") then
      if (choice:find("Freeport")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.freeport.world),
         coaches.freeport.x,
         coaches.freeport.y,
         coaches.freeport.z,
         coaches.freeport.facing
         )
      elseif (choice:find("Klick")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.klick_anon.world),
         coaches.klick_anon.x,
         coaches.klick_anon.y,
         coaches.klick_anon.z,
         coaches.klick_anon.facing
         )
      elseif (choice:find("Rivervale")) then
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
            if ((GetPlayerFlags(mySession, "admin")  or GetPlayerFlags(mySession, coach)) == "true") then
               table.insert(dialogueOptions, diag)
            end

         end
                     SendDialogue(mySession, npcDialogue, dialogueOptions)

      end
   else

      if (choice:find("Yes")) then
         npcDialogue = "Excellent, you can now use this coach any time."
         SetPlayerFlags(mySession, "bobble_by_water_coach", "true")
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      elseif (choice:find("No")) then
         npcDialogue = "If you aren't interested then why are you wasting my time."
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      else
         npcDialogue = "Would you like to sign the coachman's ledger?"
         dialogueOptions = {"Yes", "No"}
               SendDialogue(mySession, npcDialogue, dialogueOptions)

      end
   end
end


