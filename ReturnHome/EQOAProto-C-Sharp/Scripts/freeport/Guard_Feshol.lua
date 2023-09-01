function  event_say(choice)
diagOptions = {}
    npcDialogue = "This is the home of the William Nothard, High Councilor of the Iron Coffer of Freeport."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end