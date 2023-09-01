function  event_say(choice)
diagOptions = {}
    npcDialogue = "Something is wrong with the animals in the area. I believe someone is tainting the water and making them sick. There is an evil cleric known to live in the area, but no one has been able to locate her."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end