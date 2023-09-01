function  event_say(choice)
diagOptions = {}
    npcDialogue = "*The portal sits there emitting a peculiar light.*"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end