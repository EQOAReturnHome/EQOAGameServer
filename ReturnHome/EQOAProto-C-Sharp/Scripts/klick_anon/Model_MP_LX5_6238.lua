function  event_say(choice)
diagOptions = {}
    npcDialogue = "*GLIP-GLIP, BOOOOSH*"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end