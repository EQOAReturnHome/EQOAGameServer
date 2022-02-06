local playerCoaches = {
   darvar_coach = "Get me a horse to Darvar Manor.",
   seriak_coach = "Get me a horse to Ft. Seriak.",
   freeport_coach = "Get me a horse to Freeport.",
   solace_coach = "Get me a horse to the city of Dark Solace.",
   rivervale_coach = "Get me a horse to the halfling home of Rivervale"
}
local coaches = {
   Freeport = {world = 0, x = 25273.03125, y = 54.125, z = 15723.29102, facing = 3.138683081},
   Highpass = {world = 0, x = 16776.640625, y = 187.8125, z = 15351.3916015625, facing = 0.8122978806495667}
}
local ch = tostring(choice)
function event_say()
   dialogueOptions = {}
   if(GetPlayerFlags(mySession, "highpass_coach")) then
      if (ch:find("Freeport")) then
         TeleportPlayer(
         mySession,
         coaches.Freeport.world,
         coaches.Freeport.x,
         coaches.Freeport.y,
         coaches.Freeport.z,
         coaches.Freeport.facing
         )

      elseif (ch:find("Seriak")) then
         npcDialogue =
         "Off to Seriak then."
         SendDialogue(mySession, npcDialogue, dialogueOptions)
         TeleportPlayer(mySession, 0, 23359.685546875, 54.12599563598633, 19635.919921875, 1.325449824333191)
      elseif (ch:find("Solace")) then
         npcDialogue = "Off you go to Dark Solace"
         SendDialogue(mySession, npcDialogue, dialogueOptions)
         TeleportPlayer(mySession, 0, 24654.671875, 54.12599563598633, 11853.1748046875, -0.8971897959709167)
      elseif (ch:find("Rivervale")) then
         npcDialogue = "On the way to Rivervale"
         SendDialogue(mySession, npcDialogue, dialogueOptions)
         TeleportPlayer(mySession, 0, 24924.66797, 29.43331718, 9420.388672, 0.08655221)
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
      SetPlayerFlags(mySession, "highpass_coach", true)
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


