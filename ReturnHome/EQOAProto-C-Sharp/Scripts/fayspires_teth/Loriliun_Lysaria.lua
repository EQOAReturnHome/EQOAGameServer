function event_say()
diagOptions = {}
    npcDialogue = "My husband is the finest smith in Fayspires. We were amongst the last of the elves to flee Takish'Hiz, and though I long for the day we are reunited with our kind across the ocean, we stand proudly here in this final elven bastion of hope."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end