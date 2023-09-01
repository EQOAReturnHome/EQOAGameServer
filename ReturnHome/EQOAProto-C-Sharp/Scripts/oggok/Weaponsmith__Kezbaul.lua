function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can teach you weaponcrafting if you so choose. "
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end