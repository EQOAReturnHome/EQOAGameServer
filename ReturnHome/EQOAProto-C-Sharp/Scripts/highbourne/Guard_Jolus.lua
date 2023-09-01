function event_say()
diagOptions = {}
    npcDialogue = "The Tranquil Gardens are to the east and the Library Archives to the West."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end