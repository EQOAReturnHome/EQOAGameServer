-- coachman brody

local coaches = require('Scripts/ports')

local playerCoaches = {
   halas_coach = "Get me a horse to Halas.",
   murnf_coach = "Get me a horse to Murnf."
}

local dialogueOptions = {}
 
function  event_say(choice)
   if(GetPlayerFlags(mySession, "castle_lightwolf_coach") == "true") then
      if (choice:find("Halas")) then
         TeleportPlayer(mySession,GetWorld(coaches.halas.world),coaches.halas.x,coaches.halas.y,coaches.halas.z,coaches.halas.facing)
      elseif (choice:find("Murnf")) then
         TeleportPlayer(mySession,GetWorld(coaches.murnf.world),coaches.murnf.x,coaches.murnf.y,coaches.murnf.z,coaches.murnf.facing)
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
         SetPlayerFlags(mySession, "castle_lightwolf_coach","true")
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
