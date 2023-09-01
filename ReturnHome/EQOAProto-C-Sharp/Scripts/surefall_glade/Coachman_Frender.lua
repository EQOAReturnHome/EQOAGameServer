-- coachman frender

local coaches = require('Scripts/ports')

local playerCoaches = {
   darvar_manor_coach = "Get me a horse to Darvar Manor.",
   qeynos_coach = "Get me a horse to Qeynos.",
   wyndhaven_coach = "Get me a horse to Wyndhaven."
}

local dialogueOptions = {}
 
function  event_say(choice)
   if(GetPlayerFlags(mySession, "surefall_coach") == "true") then
      if (choice:find("Darvar")) then
         TeleportPlayer(mySession,GetWorld(coaches.darvar_manor.world),coaches.darvar_manor.x,coaches.darvar_manor.y,coaches.darvar_manor.z,coaches.darvar_manor.facing)
      elseif (choice:find("Qeynos")) then
         TeleportPlayer(mySession,GetWorld(coaches.qeynos.world),coaches.qeynos.x,coaches.qeynos.y,coaches.qeynos.z,coaches.qeynos.facing)
      elseif (choice:find("Wyndhaven")) then
         TeleportPlayer(mySession,GetWorld(coaches.wyndhaven.world),coaches.wyndhaven.x,coaches.wyndhaven.y,coaches.wyndhaven.z,coaches.wyndhaven.facing)
      else
         npcDialogue = "Where would you like to go?"
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
         SetPlayerFlags(mySession, "surefall_coach", "true")
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




