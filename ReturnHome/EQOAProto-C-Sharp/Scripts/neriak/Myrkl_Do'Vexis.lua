function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm sorry but I really can't be bothered at the moment. Surely someone else can assist you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end