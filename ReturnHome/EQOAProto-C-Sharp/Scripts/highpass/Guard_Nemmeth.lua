function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you have nothing to report then please be on your way."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end