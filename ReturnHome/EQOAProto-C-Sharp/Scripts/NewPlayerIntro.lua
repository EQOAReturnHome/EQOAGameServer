local ch = tostring(choice)
function event_say()
   local diagOptions = {}
   local questText = ""
   local npcDialogue = ""
--Freeport Human Magician 1001
if(GetPlayerFlags(mySession, "1001") == "0") then
    multiDialogue = { "Welcome to Freeport young magician. This is the city of trade and the eastern home of the humans.",
    "As a new magician you been invited to join the Academy of Arcane Science. They are the Magician's guild here in Freeport.",
    "Before we get too far, do you need instructions on how to use basic controls?" }
    npcDialogue = "You are now ready to move on to your first quest. Speak to Malsis, he is the guildmaster standing before you." 
    SendMultiDialogue(mySession, multiDialogue)
    SendDialogue(mySession, npcDialogue, diagOptions)
    SetPlayerFlags(mySession, "1001", "1")
end
end
--Freeport Human Alchemist 1401
--Freeport Human Bard 0501
--Freeport Human Cleric 0901
--Freeport Human Enchanter 1201
--Freeport Human Necromancer 1101
--Freeport Human Rogue 0601
--Freeport Human Shadowknight 0301
--Freeport Human Warrior 0001
--Freeport Human Wizard


