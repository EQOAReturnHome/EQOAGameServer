function  event_say(choice)
diagOptions = {}
    npcDialogue = "You want to bind your soul here I guess playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end