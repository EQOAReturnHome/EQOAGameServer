function  event_say(choice)
diagOptions = {}
    npcDialogue = "Follow the road east to Hagley Village, west to the city of Qeynos. Be mindful of the gnoll camps in the area."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end