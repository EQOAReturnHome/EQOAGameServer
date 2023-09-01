function  event_say(choice)
diagOptions = {}
    npcDialogue = "I don't believe we've met. I would love to chat with you, but my duty comes first. I apologize."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end