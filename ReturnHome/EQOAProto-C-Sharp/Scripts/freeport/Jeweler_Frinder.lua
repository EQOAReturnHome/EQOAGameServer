function  event_say(choice)
diagOptions = {}
    npcDialogue = "I see you have an interest in jewelry. Care to learn a few things about Jewelcrafting?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end