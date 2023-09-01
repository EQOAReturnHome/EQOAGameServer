function  event_say(choice)
diagOptions = {}
    npcDialogue = "My husband is always out in the mines workin'."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end