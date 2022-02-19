local ch = tostring(choice)
function event_say()
   local diagOptions = {}
   local questText = ""
   local npcDialogue = ""
   SetPlayerFlags(mySession, "EasternMagician0", true)
   if
   (mySession.MyCharacter.Class == 10 and mySession.MyCharacter.Race == 0 and mySession.MyCharacter.HumTypeNum == 0 and
   GetPlayerFlags(mySession, "EasternMagician0") and GetPlayerFlags(mySession, "EasternMagician1") == false)
   then
      npcDialogue = "Say what you must, I haven't got all day."
      diagOptions = {"I wish to become an apprentice"}
      if (ch:find("apprentice")) then
         diagOptions = {}
         npcDialogue =
         "Malsis: Oooohh, well we do require that a prospective apprentice complete a number of tasks before the enrollment."
         SendDialogue(mySession, npcDialogue, diagOptions)
         npcDialogue =
         "Malsis: Your first task is to acquire an iron ring from the merchant outside. Her name is Yulia. She wont charge you for the ring."
         SendDialogue(mySession, npcDialogue, diagOptions)
         npcDialogue = "Malsis: When you have the iron ring, return to me and I'll send you on your second task."
         SetPlayerFlags(mySession, "EasternMagician1", true)
         local questText = "You must purchase an iron ring from Merchant Yulia, then return to Malsis."
         AddQuestLog(mySession, 0, questText)
         npcDialogue = "You have received a quest!"
      end
   elseif (GetPlayerFlags(mySession, "EasternMagician1")) then
      if (CheckQuestItem(mySession, 31010, 1)) then
	    if(ch:find("actually"))then
        diagOptions = {}
        npcDialogue = "I will need an item to get started. You will need the iron ring from Merchant Yulia, then return to me."
        elseif(ch:find("Yes"))then
        diagOptions = {}
        npcDialogue = "Malsis: Wonderful. That is no ordinary ring. A small amount of power has been infused into the metal. We'll discuss more of that later."
	    SendDialogue(mySession, npcDialogue, diagOptions)
	    npcDialogue = "Malsis: Take some rest now. Return when you are ready and you shall have your next task."
        GrantXP(mySession, 430)
        DeleteQuestLog(mySession, 0)
	    else
        npcDialogue = "I take it you have the ring I sent you for?"
        diagOptions = {"Yes I do.", "Well, actually, no."}
      	SendDialogue(mySession, npcDialogue, diagOptions)
      end
     else
        diagOptions = {}
        npcDialogue = "I will need an item to get started. You will need the iron ring from Merchant Yulia, then return to me."
    end

   else
      npcDialogue =
      "Malsis: I have no time to offer odd jobs to every transient that decides to waltz into the Academy!!! I'll have you know, We are quite busy."
      end
   SendDialogue(mySession, npcDialogue, diagOptions)
end

