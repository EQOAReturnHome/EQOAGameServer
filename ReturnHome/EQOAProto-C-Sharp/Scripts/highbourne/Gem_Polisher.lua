function event_say()
diagOptions = {}
    npcDialogue = "My directives are simple. Polish gems for adventures, listen and report all conversations to my creator."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end