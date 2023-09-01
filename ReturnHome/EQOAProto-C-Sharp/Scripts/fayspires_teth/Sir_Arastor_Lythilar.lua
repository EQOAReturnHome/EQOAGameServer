function event_say()
diagOptions = {}
    npcDialogue = "Whether it be orc, dark elf, undead or dragon, we shall defend this city at any cost. No enemy shall cross our lines! The elves have surrendered too much to give any further! We must be prepared for the next battle!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end