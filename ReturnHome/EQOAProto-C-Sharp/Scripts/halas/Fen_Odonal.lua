function  event_say(choice)
diagOptions = {}
    npcDialogue = "Such a large bridge for a small village. I don't trust it, playerName...feels like it is always watching me or am I watching it?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end