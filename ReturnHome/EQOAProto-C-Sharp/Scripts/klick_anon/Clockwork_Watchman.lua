function  event_say(choice)
diagOptions = {}
    npcDialogue = "*Bzzt* \"Scout mode activated.\""
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end
