function  event_say(choice)
diagOptions = {}
    npcDialogue = "Your deeds shall prove the truth of the words you say, playerName. Cazic Thule demands we spill the blood of our enemies, the Frogloks."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end