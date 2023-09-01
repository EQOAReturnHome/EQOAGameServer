function event_say()
diagOptions = {}
    npcDialogue = "Through some special enchantments, we test the lake water, and then filter out all poisons and impurities. This does require some water to be obtained from the Plane of Sky, then blessed by our clerics. Surprisingly, just a few drops of this holy water is enough for the entire lake."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end