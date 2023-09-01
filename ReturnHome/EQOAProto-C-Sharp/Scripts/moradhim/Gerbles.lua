function  event_say(choice)
diagOptions = {}
    npcDialogue = "*Gerbles looks at you endearingly and you pet him behind his ears.*"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end