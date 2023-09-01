function  event_say(choice)
diagOptions = {}
    npcDialogue = "Let someone be foolish enough to invade. I'll 'ave their head on a stick, I will."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end