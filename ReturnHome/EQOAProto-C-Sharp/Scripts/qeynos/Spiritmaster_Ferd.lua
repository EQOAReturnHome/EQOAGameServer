function  event_say(choice)
diagOptions = {}
    npcDialogue = "Oh, hello playerName! Shall I bind you here? It might help before a big fight..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end