function  event_say(choice)
diagOptions = {}
    npcDialogue = "Beware the swamps to the south, playerName. They are festering with undead, and who knows what else. We must be on guard in case they begin to venture north."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end