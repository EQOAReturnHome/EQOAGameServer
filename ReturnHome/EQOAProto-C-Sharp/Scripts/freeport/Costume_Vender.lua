function  event_say(choice)
diagOptions = {}
    npcDialogue = "What can I offer you?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end