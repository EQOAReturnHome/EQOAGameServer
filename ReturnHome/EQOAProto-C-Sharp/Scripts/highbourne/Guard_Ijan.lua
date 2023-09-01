function event_say()
diagOptions = {}
    npcDialogue = "Have your visa ready to be reviewed at any time while you are within these walls. We have no tolerance for rule breakers and attention seekers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end