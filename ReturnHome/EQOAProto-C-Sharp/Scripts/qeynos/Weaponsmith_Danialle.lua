function  event_say(choice)
diagOptions = {}
    npcDialogue = "Are you interested in learning Weaponsmithing? Only for those willing to strike true."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end