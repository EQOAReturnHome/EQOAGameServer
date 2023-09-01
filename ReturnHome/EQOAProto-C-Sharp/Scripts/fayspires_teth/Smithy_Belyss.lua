function event_say()
diagOptions = {}
    npcDialogue = "With my armorcrafting knowledge you would have the power to protect all of your fellowship. Are you ready to learn?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end