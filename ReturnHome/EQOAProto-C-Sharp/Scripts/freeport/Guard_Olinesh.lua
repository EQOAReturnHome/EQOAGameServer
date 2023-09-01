function  event_say(choice)
diagOptions = {}
    npcDialogue = "Can't you see I'm working here?  If the commander see's me chatting, he'll have me stripped of my position.  Away with you!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end