function event_say()
diagOptions = {}
    npcDialogue = "Recording orc activities is my job. If you see anything out of the ordinary, please let me know."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end