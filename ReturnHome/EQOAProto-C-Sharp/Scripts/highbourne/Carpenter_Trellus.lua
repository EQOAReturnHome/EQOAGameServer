function event_say()
diagOptions = {}
    npcDialogue = "This is the last of the timber we've received and processed from Farny...that unambitious human woodcutter."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end