function  event_say(choice)
diagOptions = {}
    npcDialogue = "Your power is worthless without purpose behind it. Let Erollisi Marr guide your hand and thoughts."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end