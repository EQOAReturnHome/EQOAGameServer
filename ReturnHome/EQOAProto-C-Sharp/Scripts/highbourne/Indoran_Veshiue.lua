function event_say()
diagOptions = {}
    npcDialogue = "Ulria must pay for her crimes. It is only a matter of time before I find where she is hiding and retrieve the ring."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end