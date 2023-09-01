function  event_say(choice)
diagOptions = {}
    npcDialogue = "With the lake running out of fish you sometimes have to defend your catch from the other villagers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end