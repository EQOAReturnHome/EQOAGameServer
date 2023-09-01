function  event_say(choice)
diagOptions = {}
    npcDialogue = "Is this your first time here?  The keep is pretty small and easy to navigate so you shouldn't get lost."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end