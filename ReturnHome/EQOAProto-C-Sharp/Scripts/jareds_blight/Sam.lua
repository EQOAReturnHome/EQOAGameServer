function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to the Blackswan Inn. Please let me know if there is anything I can get you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end