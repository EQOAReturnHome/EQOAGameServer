function event_say()
diagOptions = {}
    npcDialogue = "Do you really want me to bind your spirit here? You have to go through the library and be exposed to all of those know-it-alls."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end