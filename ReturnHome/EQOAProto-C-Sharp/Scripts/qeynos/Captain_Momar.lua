function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keep your blade ready, wanderer. There's no telling what could be lurking outside these walls."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end