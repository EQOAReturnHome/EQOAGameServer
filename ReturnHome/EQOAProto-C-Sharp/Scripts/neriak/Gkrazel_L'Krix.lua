function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keep to yourself and you won't end up on the floor choking on your own blood."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end