function  event_say(choice)
diagOptions = {}
    npcDialogue = "No goblins in sight. Rivervale is so peaceful without them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end