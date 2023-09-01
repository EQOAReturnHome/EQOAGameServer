function  event_say(choice)
diagOptions = {}
    npcDialogue = "I have no business with you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end