function  event_say(choice)
diagOptions = {}
    npcDialogue = "I could use a drink. Not water. Got anything more�Spiritous playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end