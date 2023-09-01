function  event_say(choice)
diagOptions = {}
    npcDialogue = "There's no reason for you to be in here!! Leave immediately!!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end