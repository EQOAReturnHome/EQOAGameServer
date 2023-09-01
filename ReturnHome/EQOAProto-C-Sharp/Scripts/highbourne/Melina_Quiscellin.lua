function event_say()
diagOptions = {}
    npcDialogue = "Alchemy has been used to create all sorts of useful draughts for ages. Why not take the next logical step and apply the mixtures to offensive goals?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end