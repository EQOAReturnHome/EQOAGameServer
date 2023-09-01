function event_say()
diagOptions = {}
    npcDialogue = "A group of elves, dwarves and humans are working together to build a new set of spires on the north side of Winter's Deep Lake. You can just barely see the ships from here. It is slowly built with thousands of stone slabs being bound together with enchantments. The old spires further north have become dilapidated, and overrun with undead. Perhaps the new spires will provide a way to help bring elves across the ocean to our new home..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end