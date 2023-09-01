function  event_say(choice)
diagOptions = {}
    npcDialogue = "Well, well. Aren't you a sight for sore eyes. Surely you didn't travel all this way just to speak with me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end