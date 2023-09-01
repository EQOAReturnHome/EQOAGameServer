function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you are lost, may I recommend you follow the road south to the city of Qeynos. It is quite safe there."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end