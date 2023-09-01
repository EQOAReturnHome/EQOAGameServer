function event_say()
diagOptions = {}
    npcDialogue = "This city was constructed with intention here on Winter's Deep Lake. Long ago the Goddess blessed this lake, and it is by her magic that we have come to live here. Winter's Deep and the elven people protect one another from the evil of this land."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end