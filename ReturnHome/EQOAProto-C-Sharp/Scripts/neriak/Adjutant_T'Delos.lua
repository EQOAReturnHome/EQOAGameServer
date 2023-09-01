function  event_say(choice)
diagOptions = {}
    npcDialogue = "What's that? I'm sorry, playerName but you'll have to excuse me. Any moment left unprepared is a moment for our enemy to strike."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end