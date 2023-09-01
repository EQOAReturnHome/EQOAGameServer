function event_say()
diagOptions = {}
    npcDialogue = "To the south you'll find plenty of tailors, armorers, and the blacksmith; north of us there are grocery vendors, and to the west you'll find the docks and shipyard."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end