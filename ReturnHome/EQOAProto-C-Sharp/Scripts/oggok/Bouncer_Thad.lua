function  event_say(choice)
diagOptions = {}
    npcDialogue = "South from here is the jungle of Feerott."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end