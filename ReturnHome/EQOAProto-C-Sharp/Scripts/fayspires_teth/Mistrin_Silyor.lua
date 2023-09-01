function event_say()
diagOptions = {}
    npcDialogue = "I am here to maintain trade between the two elven cities. Though we have our separate ways of life, we benefit greatly by sharing knowledge and supplies."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end