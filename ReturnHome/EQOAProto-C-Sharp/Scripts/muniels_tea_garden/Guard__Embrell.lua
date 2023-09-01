function event_say()
diagOptions = {}
    npcDialogue = "Watch yerself if you venture out the garden after nightfall. The undead on that island are vicious."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end