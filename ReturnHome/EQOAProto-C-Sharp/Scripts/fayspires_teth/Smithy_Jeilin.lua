function event_say()
diagOptions = {}
    npcDialogue = "Here, you may learn the ways of armorcrafting, a skill passed down buy generations of elf. Are you ready to learn?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end