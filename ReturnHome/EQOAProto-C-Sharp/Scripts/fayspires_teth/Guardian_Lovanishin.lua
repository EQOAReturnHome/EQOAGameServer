function event_say()
diagOptions = {}
    npcDialogue = "Lothwin is such a poor substitute for a leader. It is I that should be master of this place. Worry not, in time my plan shall come to fruition."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end