function  event_say(choice)
diagOptions = {}
    npcDialogue = "You need me to bind you playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end