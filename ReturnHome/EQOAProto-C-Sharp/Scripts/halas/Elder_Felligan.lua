function  event_say(choice)
diagOptions = {}
    npcDialogue = "I hope our clansmen are still alive. With all of the freezeblood outside of the city, my doubts fester like a cancer."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end