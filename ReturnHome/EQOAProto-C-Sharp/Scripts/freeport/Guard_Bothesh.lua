function  event_say(choice)
diagOptions = {}
    npcDialogue = "As master of the ruling merchant house of Freeport and the Iron Coffer, the councilor retains many guards. A good many thieves pass through this city."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end