function event_say()
diagOptions = {}
    npcDialogue = "Yismay is an odd one, but his tea is the best in Tunaria."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end