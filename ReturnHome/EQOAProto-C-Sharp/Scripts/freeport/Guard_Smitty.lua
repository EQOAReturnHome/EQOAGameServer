function  event_say(choice)
diagOptions = {}
    npcDialogue = "You'll mind your words in House Nothard, or you will be receiving our immediate attention."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end