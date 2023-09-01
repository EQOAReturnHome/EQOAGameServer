function  event_say(choice)
diagOptions = {}
    npcDialogue = "One of our enemies are the many gnolls found scattered about this land. You may fight many gnolls in your time as an alchemist."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end