--coachman nioclas
local playerCoaches = {
   oggok_coach = "Get me a horse to Oggok.",
   kerplunk_outpost_coach = "Get me a horse to Kerplunk Outpost.",
   fog_marsh_coach = "Get me a horse to Fog Marsh.",
   oasis_of_marr_coach = "Get me a horse to the Oasis of Marr.",
   highpass_coach = "Get me a horse to Highpass"
}
local coaches = require('Scripts/ports')
 
function  event_say(choice)
   local dialogueOptions = {}
   if(GetPlayerFlags(mySession, "dark_solace_coach") == "true") then
      if (choice:find("Oggok")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.oggok.world),
         coaches.oggok.x,
         coaches.oggok.y,
         coaches.oggok.z,
         coaches.oggok.facing
         )
      elseif (choice:find("Kerplunk")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.kerplunk.world),
         coaches.kerplunk.x,
         coaches.kerplunk.y,
         coaches.kerplunk.z,
         coaches.kerplunk.facing
         )
      elseif (choice:find("Marsh")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.fog_marsh.world),
         coaches.fog_marsh.x,
         coaches.fog_marsh.y,
         coaches.fog_marsh.z,
         coaches.fog_marsh.facing
         )
      elseif (choice:find("Oasis")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.oasis.world),
         coaches.oasis.x,
         coaches.oasis.y,
         coaches.oasis.z,
         coaches.oasis.facing
         )
         elseif (choice:find("Highpass")) then
         TeleportPlayer(
         mySession,
         GetWorld(coaches.highpass.world),
         coaches.highpass.x,
         coaches.highpass.y,
         coaches.highpass.z,
         coaches.highpass.facing
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
      SetPlayerFlags(mySession, "dark_solace_coach", "true")
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



