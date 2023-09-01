function  event_say(choice)
diagOptions = {}
    npcDialogue = "Greetings playerName! You are more than welcome to browse my shop, but I'm terribly busy and haven't the time for chit-chat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end