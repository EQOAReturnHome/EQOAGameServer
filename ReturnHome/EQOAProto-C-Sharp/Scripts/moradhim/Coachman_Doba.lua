-- coachman doba

local coaches = require('Scripts/ports')

local playerCoaches = {
   darvar_manor_coach = "Get me a horse to Darvar Manor.",
   fayspires_coach = "Get me a horse to Fayspires.",
   halas_coach = "Get me a horse to Halas.",
   rivervale_coach = "Get me a horse to Rivervale."
}

local dialogueOptions = {}
 
function  event_say(choice)
   if(GetPlayerFlags(mySession, "moradhim_coach") == "true") then
      if (choice:find("Darvar Manor")) then
         TeleportPlayer(mySession,GetWorld(coaches.darvar_manor.world),coaches.darvar_manor.x,coaches.darvar_manor.y,coaches.darvar_manor.z,coaches.darvar_manor.facing)
      elseif (choice:find("Fayspires")) then
         TeleportPlayer(mySession,GetWorld(coaches.fayspires.world),coaches.fayspires.x,coaches.fayspires.y,coaches.fayspires.z,coaches.fayspires.facing)
      elseif (choice:find("Halas")) then
         TeleportPlayer(mySession,GetWorld(coaches.halas.world),coaches.halas.x,coaches.halas.y,coaches.halas.z,coaches.halas.facing)
      elseif (choice:find("Rivervale")) then
         TeleportPlayer(mySession,GetWorld(coaches.rivervale.world),coaches.rivervale.x,coaches.rivervale.y,coaches.rivervale.z,coaches.rivervale.facing)
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
         SetPlayerFlags(mySession, "moradhim_coach", "true")
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
