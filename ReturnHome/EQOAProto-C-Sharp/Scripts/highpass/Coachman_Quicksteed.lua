local playerCoaches = {
   darvar_manor_coach = "Get me a horse to Darvar Manor.",
   fort_seriak_coach = "Get me a horse to Ft. Seriak.",
   freeport_coach = "Get me a horse to Freeport.",
   dark_solace_coach = "Get me a horse to the city of Dark Solace.",
   rivervale_coach = "Get me a horse to the halfling home of Rivervale"
}
local coaches = require('Scripts/ports')
 
function  event_say(choice)
   local dialogueOptions = {}
   if(GetPlayerFlags(mySession, "highpass_coach") == "true") then
      if (choice:find("Freeport")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.freeport.world),
         coaches.freeport.x,
         coaches.freeport.y,
         coaches.freeport.z,
         coaches.freeport.facing
         )
      elseif (choice:find("Seriak")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.fort_seriak.world),
         coaches.fort_seriak.x,
         coaches.fort_seriak.y,
         coaches.fort_seriak.z,
         coaches.fort_seriak.facing
         )
      elseif (choice:find("Solace")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.dark_solace.world),
         coaches.dark_solace.x,
         coaches.dark_solace.y,
         coaches.dark_solace.z,
         coaches.dark_solace.facing
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
         elseif (choice:find("Darvar")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.darvar_manor.world),
         coaches.darvar_manor.x,
         coaches.darvar_manor.y,
         coaches.darvar_manor.z,
         coaches.darvar_manor.facing
         )
      else
        local npcDialogue = "Where would you like to go?"
         for coach, diag in pairs(playerCoaches) do
            if ((GetPlayerFlags(mySession, "admin")  or GetPlayerFlags(mySession, coach)) == "true") then
               table.insert(dialogueOptions, diag)
            end
         end
         SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
      end
   else
   if (choice:find("Yes")) then
      npcDialogue = "Excellent, you can now use this coach any time."
      SetPlayerFlags(mySession, "highpass_coach", "true")
         SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)

   elseif (choice:find("No")) then
      npcDialogue = "If you aren't interested then why are you wasting my time."
         SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)

   else
      npcDialogue = "Would you like to sign the coachman's ledger?"
      dialogueOptions = {"Yes", "No"}
         SendDialogue(mySession, npcDialogue, dialogueOptions, thisEntity.CharName)
   end
   end
end


