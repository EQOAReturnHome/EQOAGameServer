function  event_say(choice)
diagOptions = {}
    npcDialogue = "Other than the occasional ship passing by, not much happens on this beach. Still, it's not a bad view."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end