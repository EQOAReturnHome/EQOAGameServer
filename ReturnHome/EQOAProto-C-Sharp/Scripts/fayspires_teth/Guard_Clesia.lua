function event_say()
diagOptions = {}
    npcDialogue = "May I suggest for everyone involved that you not come through here? We know how to find you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end