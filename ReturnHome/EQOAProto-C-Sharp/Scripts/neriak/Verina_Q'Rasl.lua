function  event_say(choice)
diagOptions = {}
    npcDialogue = "Do not waste my time with your nonsense. Remove yourself from my tent."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end