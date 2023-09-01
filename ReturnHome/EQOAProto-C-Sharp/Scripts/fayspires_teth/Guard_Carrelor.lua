function event_say()
diagOptions = {}
    npcDialogue = "Many undead have been spotted just north of the lake, near an old fallen spire. I pray you do not go near there, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end