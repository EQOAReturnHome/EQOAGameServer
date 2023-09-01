function event_say()
diagOptions = {}
    npcDialogue = "I assume you can see the Bank of Highbourne before you. To the west is the Red Cliffs Inn."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end