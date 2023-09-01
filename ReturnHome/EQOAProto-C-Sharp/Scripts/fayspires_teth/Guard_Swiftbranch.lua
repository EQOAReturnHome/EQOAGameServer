function event_say()
diagOptions = {}
    npcDialogue = "Just what is that monstrosity up on the mountain in the west? Something called a telescope? Whatever is going on there can not be of this natural world."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end