function event_say()
diagOptions = {}
    npcDialogue = "Day in and out, nose down. These archives are extensive and there is always a new topic to study."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end