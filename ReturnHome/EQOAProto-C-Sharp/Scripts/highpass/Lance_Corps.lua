function  event_say(choice)
diagOptions = {}
    npcDialogue = "Hail, Adventurer!  Be it the fate of the gods that our paths have intertwined here.  Maybe we'll cross paths again one day.  Until then, take care!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end