function event_say()
diagOptions = {}
    npcDialogue = "Obviously, Indoran did not send you. Interrupting my meditations for nothing. What a waste."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end