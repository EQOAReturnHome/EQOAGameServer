local playerCoaches = {
   freeport_coach = "Get me a horse to Freeport.",
   oasis_coach = "Get me a horse to the Oasis of Marr."
}
local coaches = {
   Freeport = {world = 0, x = 25273.03125, y = 54.125, z = 15723.29102, facing = 3.138683081},
   Highpass = {world = 0, x = 16776.640625, y = 187.8125, z = 15351.3916015625, facing = 0.8122978806495667},
   Oasis =  { world = 0, x = 21109.69531, y = 54.12599564, z = 25173.51172, facing = 1.570796371}
}
local ch = tostring(choice)
function event_say()
   dialogueOptions = {}
   SetPlayerFlags(mySession, "freeport_coach", true)

   if(GetPlayerFlags(mySession, "tea_garden_coach")) then
      if (ch:find("Freeport")) then
         TeleportPlayer(
         mySession,
         coaches.Freeport.world,
         coaches.Freeport.x,
         coaches.Freeport.y,
         coaches.Freeport.z,
         coaches.Freeport.facing
         )
      elseif (ch:find("Oasis")) then
         TeleportPlayer(
         mySession,
         coaches.Oasis.world,
         coaches.Oasis.x,
         coaches.Oasis.y,
         coaches.Oasis.z,
         coaches.Oasis.facing
         )
      else
         npcDialogue = "Where would you like to go?"
         for coach, diag in pairs(playerCoaches) do
            if (GetPlayerFlags(mySession, coach)) then
               table.insert(dialogueOptions, diag)
            end
            
         end
         
                     SendDialogue(mySession, npcDialogue, dialogueOptions)

      end
   else

      if (ch:find("Yes")) then
         npcDialogue = "Excellent, you can now use this coach any time."
         SetPlayerFlags(mySession, "tea_garden_coach", true)
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


