local ch = tostring(choice)
function event_say()
   local diagOptions = {}
   local questText = ""
   local npcDialogue = ""
if(GetPlayerFlags(mySession, "10012") == "0") then
  if(ch:find("Malsis")) then
      multiDialogue = { "Kellina: Oohh, now that isn't that disappointing. Well I suppose I should instruct you. But first you must prove your worth.",
      "Kellina: Equip whatever mighty weapon you may have and slay ants. Bring me 2 cracked ant pincers as proof of your valiant deed.",
      "Kellina: After I receive the pincers I will reward you with a scroll of smoldering aura, a magician's specialty.",
      "Kellina: Now be off. I've wasted enough of my time with you." }
      SendMultiDialogue(mySession, multiDialogue)
      SetPlayerFlags(mySession, "10012", "1")
      questText = "Return two cracked ant pincers to Kellina."
      AddQuestLog(mySession, 0, questText)
      end
  elseif (GetPlayerFlags(mySession, "10012") == "1") then
      if (CheckQuestItem(mySession, 31010, 1)) then
	    if(ch:find("actually"))then
        diagOptions = {}
        npcDialogue = "I will need an item to get started. You will need the iron ring from Merchant Yulia, then return to me."
        elseif(ch:find("Yes"))then
           diagOptions = {}
           multiDialogue = { "Kellina: I suppose I can disregard your folly. You are new after all. And you did complete the task I assigned you.",
           "Kellina: I am a woman of my word. Take this scroll and study it well. The spell is paltry compared to my power, but it's a start.",
           "You have finished a quest!", "You have given away a cracked ant pincer.", "You have given away a cracked ant pincer.",
           "You have received a Smoldering Aura Scroll." ,
           "Kellina: Unfortunately I must assign you another task, but I haven't the energy to deal with a novice right now. Come back later." }
	    SendMultiDialogue(mySession, npcDialogue)
           
           GrantXP(mySession, 6900)
           DeleteQuestLog(mySession, 0)
	       SetPlayerFlags(mySession, "10012", "99")
           SetPlayerFlags(mySession, "10013", "0")
	else
           npcDialogue = "I don't remember calling for you."
           diagOptions = {"I apologize Lady, but I have the pincers.", "Yes, but…I...nevermind, sorry."}
      	    SendDialogue(mySession, npcDialogue, diagOptions)
       end
     else
        diagOptions = {}
        npcDialogue = "I will need an item to get started. You will need the iron ring from Merchant Yulia, then return to me."
    end
else
diagOptions = { "Actually, Malsis sent me."}
npcDialogue = "Kellina: I don’t have time for chit chat, dear."
SendDialogue(mySession, npcDialogue, diagOptions)
end
else
npcDialogue = "I am quite busy with my students right now. Are you sure you're in the right place?"
SendDialogue(mySession, npcDialogue, diagOptions)
end
end
