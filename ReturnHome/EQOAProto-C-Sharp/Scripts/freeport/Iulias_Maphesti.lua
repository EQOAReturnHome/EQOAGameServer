function  event_say(choice)
diagOptions = {}
    npcDialogue = "I deal with rare artifacts mostly. You probably wouldn't be interested."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end