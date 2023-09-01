function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can teach you the ways of weaponsmithing if you are interested..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end