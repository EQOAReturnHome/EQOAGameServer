function  event_say(choice)
diagOptions = {}
    npcDialogue = "Oh!!  A customer!!  How may I help you?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end