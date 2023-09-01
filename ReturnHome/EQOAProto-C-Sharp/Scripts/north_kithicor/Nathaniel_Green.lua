function event_say()
diagOptions = {}
    npcDialogue = "Nothing like a fire to warm oneself by…unless you add a flagon of mead and a hot plate of bear stew."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end