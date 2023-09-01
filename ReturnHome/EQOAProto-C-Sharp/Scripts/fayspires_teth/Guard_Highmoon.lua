function event_say()
diagOptions = {}
    npcDialogue = "Some from the trees, power so alive and green, some from the vale, on pies and mischief they are keen, some from the spires, best magic skills you have seen. Battle an elf or a halfling and you'll see what I mean."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end