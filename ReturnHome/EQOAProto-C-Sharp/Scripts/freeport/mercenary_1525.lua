function  event_say(choice)
diagOptions = {}
    npcDialogue = "Unless you have some nefarious activity to report, step aside playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end