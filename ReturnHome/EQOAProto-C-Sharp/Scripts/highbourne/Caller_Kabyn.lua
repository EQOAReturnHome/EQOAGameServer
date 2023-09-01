function event_say()
diagOptions = {}
    npcDialogue = "Every magician must start somewhere. I suggest the archives and Master Veljhan."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end