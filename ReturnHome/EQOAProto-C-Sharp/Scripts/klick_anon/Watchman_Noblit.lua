function  event_say(choice)
diagOptions = {}
    npcDialogue = "This secret cavern holds the gnomish temple of Bertoxxulous, the Lord of Decay."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end