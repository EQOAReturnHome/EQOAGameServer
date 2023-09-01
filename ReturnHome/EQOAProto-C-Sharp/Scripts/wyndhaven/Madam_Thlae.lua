function  event_say(choice)
diagOptions = {}
    npcDialogue = "Greetings, playerName. I've much to attend to, so if you wouldn't mind so terribly as to move about your business..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end