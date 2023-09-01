function  event_say(choice)
diagOptions = {}
    npcDialogue = "It is quite the trek to get here from the town of Blackwater. If you've never been there, I might be able to help you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end