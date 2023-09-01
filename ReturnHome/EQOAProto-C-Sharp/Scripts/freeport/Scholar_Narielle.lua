function  event_say(choice)
diagOptions = {}
    npcDialogue = "Ah yes, this one is fascinating� It tells of binding an air elemental to an item. You simply need the right components and cantations."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end