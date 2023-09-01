function event_say()
diagOptions = {}
    npcDialogue = "I've served so many drinks and heard far too many tall tales in my day. It started to wear on me, so Reba and I sold our bar and brothel for a new start."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end