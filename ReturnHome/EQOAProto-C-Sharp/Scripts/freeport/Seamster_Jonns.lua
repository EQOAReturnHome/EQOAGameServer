function  event_say(choice)
diagOptions = {}
    npcDialogue = "Thread working takes a steady hand and a skill eye. Are you interested in learning tailoring?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end