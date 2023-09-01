function event_say()
diagOptions = {}
    npcDialogue = "A band of our dearest elven kin were recently killed by a pack of hatebone orc thieves! They've stolen the Elddar Tomes. For the safety of all elves, we must have those tomes returned!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end