function  event_say(choice)
diagOptions = {}
    npcDialogue = "Please do not bother me playerName, I am armed with explosives and may have to act on a moments notice."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end