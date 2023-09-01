function event_say()
diagOptions = {}
    npcDialogue = "Welcome to Highbourne, traveler. Enjoy the safety the paladins provide you within these walls."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end