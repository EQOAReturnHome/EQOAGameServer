function event_say()
diagOptions = {}
    npcDialogue = "The Inn has been in my family since we first settled in the forest and started this town with the Elias, Celoon, and Barros families."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end