-- coachman lothi

local coaches = require('Scripts/ports')

local playerCoaches = {
   klick_anon_coach = "Get me a horse to Klick'Anon.",
   fort_seriak_coach = "Get me a horse to Fort Seriak.",
   freeport_coach = "Get me a horse to Freeport.",
}

local dialogueOptions = {}
 
function  event_say(choice)
   if(GetPlayerFlags(mySession, "neriak_coach") == "true") then
      if (choice:find("Klick'Anon")) then
         TeleportPlayer(mySession,GetWorld(coaches.klick_anon.world),coaches.klick_anon.x,coaches.klick_anon.y,coaches.klick_anon.z,coaches.klick_anon.facing)
      elseif (choice:find("Seriak")) then
         TeleportPlayer(mySession,GetWorld(coaches.fort_seriak.world),coaches.fort_seriak.x,coaches.fort_seriak.y,coaches.fort_seriak.z,coaches.fort_seriak.facing)
      elseif (choice:find("Freeport")) then
         TeleportPlayer(mySession,GetWorld(coaches.freeport.world),coaches.freeport.x,coaches.freeport.y,coaches.freeport.z,coaches.freeport.facing)
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
         SetPlayerFlags(mySession, "neriak_coach", "true")
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
