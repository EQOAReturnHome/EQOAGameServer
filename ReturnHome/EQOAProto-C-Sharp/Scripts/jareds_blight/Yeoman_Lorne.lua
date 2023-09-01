function  event_say(choice)
diagOptions = {}
    npcDialogue = "I am on the lookout for those bandits to the east. They've been growing in numbers as of late, with new leadership as well. I need to round up some recruits to go and thin out their camps."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end