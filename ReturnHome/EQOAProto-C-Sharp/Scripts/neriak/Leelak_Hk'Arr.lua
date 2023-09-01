function  event_say(choice)
diagOptions = {}
    npcDialogue = "If there's nothing you need from me than please be on your way."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end